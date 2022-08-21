using MeuCampeonato.Data.Context;
using MeuCampeonato.Data.Implementations;
using MeuCampeonato.Data.Repository;
using MeuCampeonato.Domain.Interfaces;
using MeuCampeonato.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeuCampeonato.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<ITeamsRepository, TeamImplementation>();

            serviceCollection.AddDbContext<MyContext>(
               options => options.UseMySql("Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=genesysjp")
           );
        }
    }
}
