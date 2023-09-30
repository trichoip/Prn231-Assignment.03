using EStore.Application.DTOs;
using EStore.Domain.Entities;

namespace EStore.Application.Services;
public interface IBookService
{
    IQueryable<Book> Entities { get; }
    Task<Book?> FindByIdAsync(int id);
    Task<BookDto> CreateAsync(BookDto entityDto);
    Task UpdateAsync(BookDto entityDto);
    Task RemoveAsync(int id);
}
