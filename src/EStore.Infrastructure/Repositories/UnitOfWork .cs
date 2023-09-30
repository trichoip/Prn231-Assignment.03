using EBookStore.Infrastructure.Repositories;
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
        AuthorRepository = new AuthorRepository(_dbContext);
        BookRepository = new BookRepository(_dbContext);
        PublisherRepository = new PublisherRepository(_dbContext);
        UserRepository = new UserRepository(_dbContext);
    }

    public IAuthorRepository AuthorRepository { get; }
    public IBookRepository BookRepository { get; }
    public IPublisherRepository PublisherRepository { get; }
    public IUserRepository UserRepository { get; }

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

