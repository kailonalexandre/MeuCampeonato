using CrossCutting.Mappings;
using MeuCampeonato.CrossCutting.DependencyInjection;
using MeuCampeonato.Domain.Dtos;
using MeuCampeonato.Domain.Interfaces.Services.Group;
using Microsoft.Extensions.DependencyInjection;

namespace MeuCampeonato.Application
{
    internal class Program
    {
        private static IMatchService? _matchService;
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureService.ConfigureDependenciesService(serviceCollection);
            ConfigureRepository.ConfigureDependenciesRepository(serviceCollection);
            
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            var autoMapper = config.CreateMapper();
            serviceCollection.AddSingleton(autoMapper);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _matchService = serviceProvider.GetService<IMatchService>();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
         
        }
        private static void Start()
        {
            List<TeamDto> teamDtos = new List<TeamDto>();
            int count = 1;
            Console.WriteLine("Digite os oito times participantes do Meu Campeonato: ");
            while (teamDtos.Count < 8)
            {
                Console.WriteLine($"Digite o time número {count}: ");
                var teamName = Console.ReadLine();
                TeamDto team = new TeamDto();
                team.Name = teamName;
                team.CreateAt = DateTime.Now;
                teamDtos.Add(team);
                count++;
            }
            _matchService.Setup(teamDtos);
            Console.ReadKey();
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Bem vindo a simulação do Meu Campeonato: ");
            Console.WriteLine("1) Iniciar Campeonato.");
            Console.WriteLine("2) Sair.");
            Console.Write("\r\n Selecione uma opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Start();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }
        }
    }
}

 