using MeuCampeonato.Domain.Entities;
using MeuCampeonato.Domain.Interfaces;
using System.Threading.Tasks;

namespace MeuCampeonato.Domain.Repository
{
    public interface IGamesRepository : IRepository<GamesEntity>
    {
    }
}
