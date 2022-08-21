using Api.Domain.Entities;
using Api.Domain.Interfaces;
using System.Threading.Tasks;

namespace Api.Domain.Repository
{
    public interface IGamesRepository : IRepository<GamesEntity>
    {
    }
}
