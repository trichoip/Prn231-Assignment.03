using EStore.Application.DTOs;

namespace EStore.Application.Services;

public interface ICategoryService
{
    Task<IList<CategoryDto>> FindAllAsync();
    Task<CategoryDto?> FindByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CategoryDto entityDto);
    Task UpdateAsync(CategoryDto entityDto);
    Task RemoveAsync(int id);
}
