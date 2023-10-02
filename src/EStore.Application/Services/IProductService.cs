using EStore.Application.DTOs;

namespace EStore.Application.Services;
public interface IProductService
{
    Task<IList<ProductDto>> FindAllAsync();
    Task<ProductDto?> FindByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductDto entityDto);
    Task UpdateAsync(ProductDto entityDto);
    Task RemoveAsync(int id);
}
