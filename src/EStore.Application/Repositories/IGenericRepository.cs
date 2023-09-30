namespace EStore.Application.Repositories;
public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Entities { get; }
    Task<IList<T>> FindAllAsync();
    Task<T?> FindByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}
