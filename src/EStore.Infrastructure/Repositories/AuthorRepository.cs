using EBookStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;
    public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
