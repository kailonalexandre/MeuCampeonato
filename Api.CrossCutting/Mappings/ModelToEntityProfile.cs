using MeuCampeonato.Domain.Entities;
using AutoMapper;
using Domain.Models;

namespace CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<TeamsEntity, TeamsModel>()
                .ReverseMap();

            CreateMap<MatchEntity, MatchsModel>()
                .ReverseMap();
        }
    }
}
