using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EStore.Infrastructure.Repositories;
public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _dbContext;
    public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<OrderDetail>> FindAllAsync(Expression<Func<OrderDetail, bool>>? expression = null)
    {
        var query = _dbContext.OrderDetails.AsQueryable();
        if (expression != null) query = query.Where(expression);
        return await query.ToListAsync();
    }

    public async Task<OrderDetail?> FindByIdAsync(object?[] ids)
    {
        return await _dbContext.OrderDetails.FindAsync(ids);
    }

    public Task RemoveRangeAsync(IList<OrderDetail> orderDetails)
    {
        _dbContext.RemoveRange(orderDetails);
        return Task.CompletedTask;
    }
}
