using EStore.Domain.Entities;
using System.Linq.Expressions;

namespace EStore.Application.Repositories;
public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task RemoveRangeAsync(IList<OrderDetail> orderDetails);
    Task<IList<OrderDetail>> FindAllAsync(Expression<Func<OrderDetail, bool>>? expression = null);
    Task<OrderDetail?> FindByIdAsync(object?[] ids);

}
