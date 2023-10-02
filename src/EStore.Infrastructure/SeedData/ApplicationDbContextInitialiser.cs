using AutoBogus;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using EStore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Claims;

namespace EStore.Infrastructure.SeedData;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
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

        var administratorRole = new IdentityRole<int>() { Name = "Administrator" };
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "developermode549@gmail.com", Email = "developermode549@gmail.com", EmailConfirmed = true, PhoneNumber = "0123456789" };
        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "aA123456!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
            await _userManager.AddClaimAsync(administrator, new Claim(ClaimTypes.Country, "HCM"));
        }

        var categories = new AutoFaker<Category>().Configure(configure =>
        {
            configure.WithTreeDepth(1);
            configure.WithRepeatCount(0);
        })
         .Ignore(a => a.CategoryId)
         .Generate(5);

        var products = new AutoFaker<Product>().Configure(configure =>
        {
            configure.WithTreeDepth(1);
            configure.WithRepeatCount(0);
            configure.WithSkip<Product>(a => a.ProductId);
            configure.WithSkip<Product>(a => a.CategoryId);
        })
        .RuleFor(c => c.Category, f => f.Random.CollectionItem(categories))
        //.RuleFor(c => c.ProductId, f => f.IndexFaker + 1)
        .Generate(20);

        var orderDetails = new AutoFaker<OrderDetail>().Configure(configure =>
        {
            configure.WithTreeDepth(1);
            configure.WithSkip<OrderDetail>(a => a.OrderId);
            configure.WithSkip<OrderDetail>(a => a.ProductId);
        })
        .RuleFor(c => c.Product, f => f.Random.CollectionItem(products));

        var orders = new AutoFaker<Order>().Configure(configure =>
        {
            configure.WithTreeDepth(1);
            configure.WithSkip<Order>(a => a.OrderId);
            configure.WithSkip<Order>(a => a.MemberId);
        })
        .RuleFor(c => c.MemberId, f => administrator.Id)
        //.RuleFor(c => c.OrderDetails, f => orderDetails.Generate(5).DistinctBy(c => c.Product.ProductId).ToList())
        .RuleFor(c => c.OrderDetails, f => orderDetails.Generate(5).DistinctBy(c => c.Product.UnitsInStock).ToList())
        .Generate(5);

        await _context.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }
}
