using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    //implementando el repository generico, usandolo con la clase que se desea
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        //metodo que realiza busqueda de video por nombre
        Task<Video> GetVideoByName(string nameVideo);
        //obtener videos registrados por un usuario
        Task<IEnumerable<Video>> GetVideoByUserName(string username);
    }
}
