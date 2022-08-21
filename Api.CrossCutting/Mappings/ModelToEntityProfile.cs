using Api.Domain.Entities;
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

            CreateMap<GamesEntity, GamesModel>()
                .ReverseMap();
        }
    }
}
