using EStore.Application.DTOs;
using EStore.Domain.Entities;

namespace EStore.Application.Services;
public interface IUserService
{
    IQueryable<User> Entities { get; }
    Task<User?> FindByIdAsync(int id);
    Task<UserDto> CreateAsync(UserDto entityDto);
    Task UpdateAsync(UserDto entityDto);
    Task RemoveAsync(int id);
}
