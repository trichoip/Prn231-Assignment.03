using EStore.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EStore.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

}
