namespace EStore.Application.Repositories;
public interface IUnitOfWork : IDisposable
{
    IAuthorRepository AuthorRepository { get; }
    IBookRepository BookRepository { get; }
    IPublisherRepository PublisherRepository { get; }
    IUserRepository UserRepository { get; }
    IQueryable<T> Entities<T>() where T : class;
    Task CommitAsync();
}
