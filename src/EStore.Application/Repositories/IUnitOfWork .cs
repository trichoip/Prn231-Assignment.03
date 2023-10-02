namespace EStore.Application.Repositories;
public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderDetailRepository OrderDetailRepository { get; }
    IQueryable<T> Entities<T>() where T : class;
    Task CommitAsync();
}
