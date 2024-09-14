using AutoMapper;
using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
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
            //creacion de mapper de CreateDirectorCommand a Director
            CreateMap<CreateDirectorCommand, Director>();
            //creacion de mapper de UpdateStreamerCommand a streamer
            CreateMap<UpdateStreamerCommand, Streamer>();
        }
    }
}
