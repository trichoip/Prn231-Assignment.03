using EStore.Application.DTOs;

namespace EStore.Application.Services;
public interface IOrderService
{
    Task<IList<OrderDto>> FindAllAsync();
    Task<OrderDto?> FindByIdAsync(int id);
    Task<OrderDto> CreateAsync(OrderDto entityDto);
    Task UpdateAsync(OrderDto entityDto);
    Task RemoveAsync(int id);
}
