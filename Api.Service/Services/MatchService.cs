using MeuCampeonato.Domain.Dtos;
using MeuCampeonato.Domain.Interfaces.Services.Group;
using Domain.Dtos;
using Domain.Utils;
using IronPython.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using MeuCampeonato.Domain.Repository;
using AutoMapper;
using Domain.Models;
using MeuCampeonato.Domain.Entities;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Service.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchsRepository _matchsRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly IMapper _mapper;

        private List<bracket> brackets = new List<bracket>();
        private Dictionary<int, TeamDto> podium = new Dictionary<int, TeamDto>();
        private Dictionary<TeamDto, int> teamScore = new Dictionary<TeamDto, int>();


        public MatchService(IMatchsRepository matchsRepository, ITeamsRepository teamsRepository, IMapper mapper)
        {
            _matchsRepository = matchsRepository;
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        public async Task Setup(List<TeamDto> teamDtos)
        {
            brackets.Clear();
            teamScore.Clear();
            GenerateBracket(teamDtos, BracketEnum.QuarterFinals);
            await SetTeams(teamDtos);
            await StartQuarterFinal();
            await StartSemiFinal();
            await StartFinal();
            await RecapMatch();
            Results();
        }
        private async Task SetTeams(List<TeamDto> teamDtos)
        {
            foreach(TeamDto team in teamDtos)
            {
                teamScore.Add(team, 0);
                var teamModel = _mapper.Map<TeamsModel>(team);
                var teamEntity = _mapper.Map<TeamsEntity>(teamModel);
                await _teamsRepository.InsertAsync(teamEntity);
            }

        }

        private void Results()
        {
            Console.WriteLine();
            Console.WriteLine(@$"O Meu Campeonato terminou!.
                                Estes são os resultados:
                                Primeiro lugar: {podium[1].Name}
                                Segundo lugar: {podium[2].Name}
                                Terceiro lugar: {podium[3].Name}");
                                
        }

        private async Task StartFinal() 
        {
            var winner = new TeamDto();
            var loser = new TeamDto();
            Console.WriteLine("\r\nComeçando a final!");
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.EndGame).ToList())
            {
                Console.WriteLine($"\r\n{bracket.TeamA.Name} vs {bracket.TeamB.Name}");
                winner = await StartMatchs(bracket);
                loser = winner == bracket.TeamA ? bracket.TeamB : bracket.TeamA;
            }
            podium.Add(1, winner);
            podium.Add(2, loser);
        }

        private async Task RecapMatch()
        {
            var winner = new TeamDto();
            Console.WriteLine("\r\nComeçando a repescagem!");
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.Losers).ToList())
            {
               Console.WriteLine($"\r\n{bracket.TeamA.Name} vs {bracket.TeamB.Name}");
                winner = await StartMatchs(bracket);
            }
            podium.Add(3, winner);
        }

        private async Task StartSemiFinal()
        {
            List<TeamDto> Winners = new List<TeamDto>();
            List<TeamDto> Losers = new List<TeamDto>();
            Console.WriteLine("\r\nComeçando as semifinais!");
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.SemiFinal).ToList())
            {
               Console.WriteLine($"\r\n{bracket.TeamA.Name} vs {bracket.TeamB.Name}");
                var winner = await StartMatchs(bracket);
                Winners.Add(winner);
                Losers.Add(bracket.TeamA == winner ? bracket.TeamB : bracket.TeamA);
            }

            var winnersArray = Winners.ToArray();
            var losersArray = Losers.ToArray();

            brackets.Add(new bracket()
            {
                TeamA = winnersArray[0],
                TeamB = winnersArray[1],
                Bracket = BracketEnum.EndGame
            });

            brackets.Add(new bracket()
            {
                TeamA = losersArray[0],
                TeamB = losersArray[1],
                Bracket = BracketEnum.Losers
            });
        }

        private async Task StartQuarterFinal()
        {
            List<TeamDto> Winners = new List<TeamDto>();
            Console.WriteLine("\r\nComeçando as quartas de finais!");
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.QuarterFinals).ToList())
            {
               Console.WriteLine($"\r\n{bracket.TeamA.Name} vs {bracket.TeamB.Name}");
                var winner = await StartMatchs(bracket);
                Winners.Add(winner);
            }

            var winnersArray = Winners.ToArray();

            brackets.Add(new bracket()
            {
                TeamA = winnersArray[0],
                TeamB = winnersArray[1],
                Bracket = BracketEnum.SemiFinal
            }); 
           
            brackets.Add(new bracket()
            {
                TeamA = winnersArray[2],
                TeamB = winnersArray[3],
                Bracket = BracketEnum.SemiFinal
            });
            
        }

        public bool Desempate()
        {
            return false;
        }

        public struct bracket
        {
            public TeamDto TeamA;
            public TeamDto TeamB;
            public BracketEnum Bracket;
        }

        /// <summary>
        /// Gera o chaveamento de times.
        /// </summary>
        /// <param name="Teams"></param>
        public void GenerateBracket(List<TeamDto> Teams, BracketEnum actualBracket)
        {
            List<int> UsedTeams = new List<int>();
      
            while (UsedTeams.Count < Teams.Count)
            {
                (TeamDto teamA, TeamDto teamB) = GenerateTeamCombination(Teams, UsedTeams);
                brackets.Add(new bracket()
                {
                    TeamA = teamA,
                    TeamB = teamB,
                    Bracket = actualBracket
                });
            }
                
        }
        /// <summary>
        /// Gera uma combinação de times para serem adicionados na chave
        /// </summary>
        /// <param name="Teams"></param>
        /// <param name="UsedTeams"></param>
        /// <returns></returns>
        public (TeamDto teamA, TeamDto teamB) GenerateTeamCombination(List<TeamDto> Teams, List<int> UsedTeams)
        {
            var teamAindex = RandomTeam(Teams);
            var teamBindex = RandomTeam(Teams);
            if (UsedTeams.Contains(teamAindex) || UsedTeams.Contains(teamBindex) || teamAindex == teamBindex)
                return GenerateTeamCombination(Teams, UsedTeams);    
            else
            {
                UsedTeams.Add(teamAindex);
                UsedTeams.Add(teamBindex);
            }    
            return (Teams[teamAindex], Teams[teamBindex]);
        }

        /// <summary>
        /// Seleciona uma posição aleatória de um time dentro da lista.
        /// </summary>
        /// <param name="Teams"></param>
        /// <returns></returns>
        private int RandomTeam(List<TeamDto> Teams)
        {
           Random rng = new Random();
           return rng.Next(0, Teams.Count);
        }

        /// <summary>
        /// Começa as partidas e retorna o time vencedor.
        /// </summary>
        public async Task <TeamDto> StartMatchs(bracket Bracket, bool Draw = false)
        {
            string[] scores = ExecutePythonScript();
            
            MatchDto Match = new MatchDto();
            Match.TeamA = Bracket.TeamA.Name;
            Match.TeamB = Bracket.TeamB.Name;
            Match.Bracket = Bracket.Bracket;
            Match.ScoreTeamA = Convert.ToInt32(scores[0]);
            Match.ScoreTeamB = Convert.ToInt32(scores[1]);
            var winner = Match.ScoreTeamA > Match.ScoreTeamB ? Bracket.TeamA : Bracket.TeamB;
            var loser = winner == Bracket.TeamA ? Bracket.TeamB : Bracket.TeamA;

            if (teamScore.ContainsKey(winner))
            {
                teamScore[winner] += winner == Bracket.TeamA ? Match.ScoreTeamA : Match.ScoreTeamB;
                teamScore[winner] -= winner == Bracket.TeamA ? Match.ScoreTeamB : Match.ScoreTeamA;  
            }
            if (teamScore.ContainsKey(loser))
            {
                teamScore[loser] += loser == Bracket.TeamA ? Match.ScoreTeamA : Match.ScoreTeamB;
                teamScore[loser] -= loser == Bracket.TeamA ? Match.ScoreTeamB : Match.ScoreTeamA;
            }

            if (Match.ScoreTeamA == Match.ScoreTeamB)
            {
                if (Draw)
                    winner = DrawMatch(Bracket);
                else
                    winner = await StartMatchs(Bracket, true);
            }
            var matchModel = _mapper.Map<MatchsModel>(Match);
            var matchEntity = _mapper.Map<MatchEntity>(matchModel);
            await _matchsRepository.InsertAsync(matchEntity);

            Console.WriteLine($"Vencedor {winner.Name} | Pontuação {teamScore[winner]}");
            Console.WriteLine($"Perdedor {loser.Name} | Pontuação {teamScore[loser]}");

            return winner;

        }
        public TeamDto DrawMatch(bracket bracket) => bracket.TeamA.CreateAt < bracket.TeamB.CreateAt ? bracket.TeamA : bracket.TeamB;
        


        /// <summary>
        /// Executa o script Python.
        /// </summary>
        /// <returns></returns>
        public string[] ExecutePythonScript()
        {
            var engine = Python.CreateEngine();

            var script = Path.Combine(Constants._scriptPath, "teste.py");
            var source = engine.CreateScriptSourceFromFile(script);

            var argv = new List<string>();
            argv.Add("");

            engine.GetSysModule().SetVariable("argv", argv);

            var eIO = engine.Runtime.IO;

            var errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            var results = new MemoryStream();
            eIO.SetOutput(results, Encoding.Default);

            var scope = engine.CreateScope();
            source.Execute(scope);

            string str(byte[] x) => Encoding.Default.GetString(x);
            return str(results.ToArray()).Replace("\r\n", " ").Trim().Split(" ");
        }
        
    }
}
