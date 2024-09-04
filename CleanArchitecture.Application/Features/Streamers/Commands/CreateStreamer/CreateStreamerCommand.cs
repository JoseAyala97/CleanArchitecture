using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    //libreria mediaTR con valor que retornara 
    public class CreateStreamerCommand : IRequest<int>
    {
        //Propiedades que se crearan
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
