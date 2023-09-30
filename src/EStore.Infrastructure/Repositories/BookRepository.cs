using EBookStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;

namespace EStore.Infrastructure.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
