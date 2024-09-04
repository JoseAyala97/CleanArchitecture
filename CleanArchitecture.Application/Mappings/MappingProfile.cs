﻿using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mappings
{
    //hereda de libreria de automapper: Profile
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //creacion de mapper con entidad video a videoVm
            CreateMap<Video, VideosVm>();
            //creacion de mapper de StreamerCommand a Streamer
            CreateMap<CreateStreamerCommand, Streamer>();
        }
    }
}
