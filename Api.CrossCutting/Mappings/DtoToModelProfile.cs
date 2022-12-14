using MeuCampeonato.Domain.Dtos;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<TeamsModel, TeamDto>()
                .ReverseMap();
            CreateMap<MatchsModel, MatchDto>()
               .ReverseMap();
        }
    }
}
