using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
    {
        //al no tener metodos propios, se genera solo el constructor para instanciar y realizar la conexion con la base de datos
        public StreamerRepository(StreamerDbContext context) : base(context)
        {
        }
    }
}
