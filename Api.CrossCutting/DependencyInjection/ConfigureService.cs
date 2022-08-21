using MeuCampeonato.Domain.Interfaces.Services.Group;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace MeuCampeonato.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
           serviceCollection.AddTransient<IGameService, GameService>();
        }
    }
}
