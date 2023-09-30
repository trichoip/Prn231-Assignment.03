using EStore.Application.DTOs;
using EStore.Domain.Entities;

namespace EStore.Application.Services;
public interface IPublisherService
{
    IQueryable<Publisher> Entities { get; }
    Task<Publisher?> FindByIdAsync(int id);
    Task<PublisherDto> CreateAsync(PublisherDto entityDto);
    Task UpdateAsync(PublisherDto entityDto);
    Task RemoveAsync(int id);
}
