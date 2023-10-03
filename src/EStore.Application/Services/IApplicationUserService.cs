using EStore.Application.DTOs;

namespace EStore.Application.Services;
public interface IApplicationUserService
{
    Task<IList<ApplicationUserDto>> FindAllAsync();
    Task<ApplicationUserDto?> FindByIdAsync(int id);
    Task<ApplicationUserDto> CreateAsync(ApplicationUserDto entityDto);
    Task UpdateAsync(ApplicationUserDto entityDto);
    Task RemoveAsync(int id);
}
