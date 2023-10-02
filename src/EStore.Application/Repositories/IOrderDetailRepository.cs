using EStore.Domain.Entities;

namespace EStore.Application.Repositories;
public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task RemoveRangeAsync(IList<OrderDetail> orderDetails);
}
