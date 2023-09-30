using EBookStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PublisherRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
