using Api.Domain.Dtos;
using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services.Client
{
    public interface IClientService
    {
        Task<TeamsEntity> Get(Guid id);
        Task<IEnumerable<TeamsEntity>> GetAll();
        Task<TeamsEntity> Post(TeamDto team);

    }
}
