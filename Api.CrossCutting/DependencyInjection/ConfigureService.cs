using Api.Domain.Interfaces.Services.Client;
using Api.Domain.Interfaces.Services.Group;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient<IUserService, UserService>();
           // serviceCollection.AddTransient<ILoginService, LoginService>();
            //serviceCollection.AddTransient<IClientService, ClientService>();
           // serviceCollection.AddTransient<IGameService, GroupService>();

        }
    }
}
