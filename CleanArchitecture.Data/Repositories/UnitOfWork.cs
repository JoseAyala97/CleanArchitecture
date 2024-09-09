using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    //metodos de IUnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        //objeto collection porque alojara todos los otros repostiroes
        private Hashtable _repositories;
        private readonly StreamerDbContext _dbContext;
        public UnitOfWork(StreamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //dispara la confirmacion de todas las transacciones
        public async Task<int> Complete()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Err" ,ex);
            }
           
        }
        //que se elimine el context cuando la transaccion haya terminado
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        //instancia del repositorio
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if(_repositories == null)
                //si es nula, crea la conexion
                _repositories = new Hashtable();

            //capturar el nombre de la entidad que se esta trabajando
            var type = typeof(TEntity).Name;
            //sino existe, agregalo
            if (!_repositories.ContainsKey(type))
            {
                //como no existe lo agregara, pero primero debera crearlo
                var repositoryType = typeof(RepositoryBase<>);
                //llamado a su instancia
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                //ahora solo debe agregarse la instancia
                _repositories.Add(type, repositoryInstance);
            }
            //retornar toda la lista de repositorios
            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
