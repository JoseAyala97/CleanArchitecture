using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    //IDisposable para poder usar UnitOfWork
    public interface IUnitOfWork : IDisposable
    {
        //metood generico que devuelva la isntancia del servicio repositorio que se desee utilizar
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        //Metodo para saber cuando una transaccion ha terminado
        Task<int> Complete();
    }
}
