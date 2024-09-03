using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    //IRequest libreria MediatR - informando el tipo de data que se desea a devolver
    public class GetVideosListQuery : IRequest<List<VideosVm>>
    {
        public string _UserName { get; set; } = string.Empty
        public GetVideosListQuery(string? username)
        {
            //en caso de que no exista, debe lanzar una exception, sintaxis: ?? throw new ArgumentNullException(nameof(username));
            _UserName = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
