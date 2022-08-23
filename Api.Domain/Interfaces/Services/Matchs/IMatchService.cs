using MeuCampeonato.Domain.Dtos;
using MeuCampeonato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuCampeonato.Domain.Interfaces.Services.Group
{
    public interface IMatchService
    {
        Task Setup(List<TeamDto> teamDtos);
    }
}
