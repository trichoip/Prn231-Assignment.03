using EStore.Application.Repositories;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> dbSet;
    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<T>();
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);

    public async Task<IList<T>> FindAllAsync() => await dbSet.ToListAsync();

    public async Task<T?> FindByIdAsync(int id) => await dbSet.FindAsync(id);

    public Task RemoveAsync(T entity) => Task.FromResult(dbSet.Remove(entity));

    public Task UpdateAsync(T entity) => Task.FromResult(dbSet.Update(entity));
}
