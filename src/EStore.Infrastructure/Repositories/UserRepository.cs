using EBookStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
