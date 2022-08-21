using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Group
{
    public interface IGameService
    {
        Task<GamesEntity> Get(Guid id);
        Task<IEnumerable<GamesEntity>> GetAll();
        Task<GamesEntity> Post(GamesEntity game);
    }
}
