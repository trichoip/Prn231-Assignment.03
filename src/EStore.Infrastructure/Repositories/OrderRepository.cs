using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
