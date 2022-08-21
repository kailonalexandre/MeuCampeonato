using MeuCampeonato.CrossCutting.DependencyInjection;
using MeuCampeonato.Domain.Dtos;
using MeuCampeonato.Domain.Interfaces.Services.Group;
using Microsoft.Extensions.DependencyInjection;

namespace MeuCampeonato.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureService.ConfigureDependenciesService(serviceCollection);
            ConfigureRepository.ConfigureDependenciesRepository(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var gameService = serviceProvider.GetService<IGameService>();

            List<TeamDto> teamDtos = new List<TeamDto>();
            Console.WriteLine("Digite os times participantes do Meu Campeonato: ");
            while(teamDtos.Count < 8)
            {
                var teamName = Console.ReadLine();
                TeamDto team = new TeamDto();
                team.TeamName = teamName;
                team.CreateAt = DateTime.Now;
                teamDtos.Add(team);
            }


            gameService.Setup(teamDtos);
        }
    }
}

 