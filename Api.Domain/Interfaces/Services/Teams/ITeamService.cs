using MeuCampeonato.Domain.Dtos;
using MeuCampeonato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuCampeonato.Domain.Interfaces.Services.Client
{
    public interface ITeamService
    {
        Task<TeamsEntity> Get(Guid id);
        Task<IEnumerable<TeamsEntity>> GetAll();
        Task<TeamsEntity> Post(TeamDto team);

    }
}
