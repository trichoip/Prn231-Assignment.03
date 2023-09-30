using EStore.Application.DTOs;
using EStore.Domain.Entities;

namespace EStore.Application.Services;

public interface IAuthorService
{
    IQueryable<Author> Entities { get; }
    Task<Author?> FindByIdAsync(int id);
    Task<AuthorDto> CreateAsync(AuthorDto entityDto);
    Task UpdateAsync(AuthorDto entityDto);
    Task RemoveAsync(int id);
}
