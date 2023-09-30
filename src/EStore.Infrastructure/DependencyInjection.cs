using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Infrastructure.Data;
using EStore.Infrastructure.Repositories;
using EStore.Infrastructure.SeedData;
using EStore.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
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
    }

    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IBookService, BookService>()
            .AddScoped<IPublisherService, PublisherService>()
            .AddScoped<IUserService, UserService>();
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
                options => options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("Prn231As03DB"));
        }

    }

    public static void AddInitialiseDatabase(this IServiceCollection services)
    {
        services
            .AddScoped<ApplicationDbContextInitialiser>();
    }

    public static async Task UseInitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }

}
