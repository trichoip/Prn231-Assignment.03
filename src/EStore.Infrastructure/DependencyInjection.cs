using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;
using EStore.Infrastructure.Data;
using EStore.Infrastructure.Repositories;
using EStore.Infrastructure.SeedData;
using EStore.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EStore.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddServices();
        services.AddInitialiseDatabase();
        services.AddDefaultIdentity();
    }

    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddDefaultIdentity();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IApplicationUserService, ApplicationUserService>()
            .AddScoped<IOrderDetailService, OrderDetailService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration["DatabaseProvider"] == "MySql")
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!)
                .UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("Prn231As03DB")
                .UseLazyLoadingProxies());
        }

    }

    public static void AddInitialiseDatabase(this IServiceCollection services)
    {
        services
            .AddScoped<ApplicationDbContextInitialiser>();
    }

    public static void AddDefaultIdentity(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequiredUniqueChars = 0;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
            options.Lockout.MaxFailedAccessAttempts = 2;
        })
          .AddRoles<IdentityRole<int>>()
          .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    public static async Task UseInitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }

}
