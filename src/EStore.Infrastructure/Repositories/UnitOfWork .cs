using EStore.Application.Repositories;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private bool disposed = false;
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        CategoryRepository = new CategoryRepository(_dbContext);
        OrderRepository = new OrderRepository(_dbContext);
        ProductRepository = new ProductRepository(_dbContext);
        OrderDetailRepository = new OrderDetailRepository(_dbContext);
        ApplicationUserRepository = new ApplicationUserRepository(_dbContext);
    }

    public ICategoryRepository CategoryRepository { get; private set; }

    public IOrderRepository OrderRepository { get; private set; }

    public IProductRepository ProductRepository { get; private set; }

    public IOrderDetailRepository OrderDetailRepository { get; private set; }

    public IApplicationUserRepository ApplicationUserRepository { get; private set; }

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
    public IQueryable<T> Entities<T>() where T : class => _dbContext.Set<T>();

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}

