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

namespace Service.Services
{
    public class GameService : IGameService
    {
        private List<bracket> brackets = new List<bracket>();
        private Dictionary<int, TeamDto> podium = new Dictionary<int, TeamDto>();
        public void Setup(List<TeamDto> teamDtos)
        {
            brackets.Clear();
            GenerateBracket(teamDtos, BracketEnum.QuartasDeFinal);
            ComecarQuartasDeFinal();
            ComecarSemiFinal();
            ComecarFinal();
            RecapMatch();
            Results();
        }

        private void Results()
        {
            Console.WriteLine(@$"O Meu Campeonato terminou!.
                                Estes são os resultados:
                                Primeiro lugar: {podium[1].TeamName}
                                Segundo lugar: {podium[2].TeamName}
                                Terceiro lugar: {podium[3].TeamName}");
                                
        }

        private void ComecarFinal() 
        {
            var winner = new TeamDto();
            var loser = new TeamDto();
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.Final).ToList())
            {
                winner = StartMatchs(bracket);
            }
            podium.Add(1, winner);
            podium.Add(2, loser);
        }

        private void RecapMatch()
        {
            var winner = new TeamDto();
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.Perdedores).ToList())
            {
                winner = StartMatchs(bracket);
            }
            podium.Add(3, winner);
        }

        private void ComecarSemiFinal()
        {
            List<TeamDto> Winners = new List<TeamDto>();
            List<TeamDto> Losers = new List<TeamDto>();
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.SemiFinal).ToList())
            {
                var winner = StartMatchs(bracket);
                Winners.Add(winner);
                Losers.Add(bracket.TeamA == winner ? bracket.TeamB : bracket.TeamA);
            }

            var winnersArray = Winners.ToArray();
            var losersArray = Losers.ToArray();

            brackets.Add(new bracket()
            {
                TeamA = winnersArray[0],
                TeamB = winnersArray[1],
                Bracket = BracketEnum.Final
            });

            brackets.Add(new bracket()
            {
                TeamA = losersArray[0],
                TeamB = losersArray[1],
                Bracket = BracketEnum.Perdedores
            });
        }

        private void ComecarQuartasDeFinal()
        {
            List<TeamDto> Winners = new List<TeamDto>();
            foreach (var bracket in brackets.Where(p => p.Bracket == BracketEnum.QuartasDeFinal).ToList())
            {
                var winner = StartMatchs(bracket);
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
        public TeamDto StartMatchs(bracket Bracket)
        {
            string[] scores = ExecutePythonScript();
            
            GamesDto Match = new GamesDto();
            Match.FirstTeam = Bracket.TeamA.TeamName;
            Match.SecondTeam = Bracket.TeamB.TeamName;
            Match.GoalsFistTeam = Convert.ToInt32(scores[0]);
            Match.GoalsSecondTeam = Convert.ToInt32(scores[1]);
            
            //TODO:Desempate
            //TODO:Salvar no banco
            return Match.GoalsFistTeam > Match.GoalsSecondTeam ? Bracket.TeamA : Bracket.TeamB; 
        }


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
