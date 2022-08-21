﻿using Api.Domain.Dtos;
using Api.Domain.Entities;
using AutoMapper;
using Domain.Dtos;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<TeamDto, TeamsEntity>()
                .ReverseMap();
            
            CreateMap<GamesDto, GamesEntity>()
                .ReverseMap();
        }
    }
}