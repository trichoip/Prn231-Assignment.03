using AutoBogus;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.EntityFrameworkCore.Infrastructure.Internal;

namespace EStore.Infrastructure.SeedData;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.ProviderName == typeof(MySQLOptionsExtension)!.Assembly.GetName().Name)
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.MigrateAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {

        var entities = AutoFaker.Generate<Book>(3, configure =>
        {
            configure.WithRepeatCount(2);
            configure.WithRecursiveDepth(1);

            configure.WithSkip<Author>(a => a.AuthorId);
            configure.WithSkip<User>(a => a.UserId);
            configure.WithSkip<Book>(a => a.BookId);
            configure.WithSkip<Publisher>(a => a.PubId);
            configure.WithSkip<Role>(a => a.RoleId);
        });

        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
