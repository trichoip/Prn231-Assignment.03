using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _dbContext;
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task RemoveRangeAsync(IList<OrderDetail> orderDetails)
    {
        _dbContext.RemoveRange(orderDetails);
        return Task.CompletedTask;
    }
}
