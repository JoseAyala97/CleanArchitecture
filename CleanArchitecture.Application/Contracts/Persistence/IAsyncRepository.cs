using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    //permite dar mantenimiento a todas las entidades del proyecto
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        //IReadOnlyList , T indica que sera de tipo generico
        Task<IReadOnlyList<T>> GetAllAsync();
        //exprressionFunctions -> servira para realizar la consulta | se transformara en consulta SQL
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        //consulta con ordenamiento
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,string includeString =null, bool disableTracking = true);
        //consulta con paginacion
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            string includeString =null, 
            bool disableTracking = true);
        //Get por id 
        Task<T> GetByIdAsync(int id);
        //crear cualquier entidad
        Task<T> AddAsync(T entity);
        //Actualizar cualqueir entidad
        Task<T> UpdateAsync(T entity);
        //Eliminar cualquier registro
        Task DeleteAsync(T entity);
    }
}
