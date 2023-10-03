using EStore.Application.DTOs;
using EStore.Domain.Entities;
using System.Linq.Expressions;

namespace EStore.Application.Services;
public interface IOrderDetailService
{
    Task<IList<OrderDetailDto>> FindAllAsync(Expression<Func<OrderDetail, bool>>? expression = null);
    Task<OrderDetailDto?> FindByIdAsync(object?[] ids);
    Task<OrderDetailDto> CreateAsync(OrderDetailDto entityDto);
    Task UpdateAsync(OrderDetailDto entityDto);
    Task RemoveAsync(Expression<Func<OrderDetail, bool>> expression);
}
