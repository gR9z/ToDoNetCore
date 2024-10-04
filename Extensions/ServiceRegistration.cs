using TodoNetCore.Data.Repository;
using TodoNetCore.Data.Repository.Impl;
using TodoNetCore.Services.Todo;

namespace TodoNetCore.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<ITodoRepository, TodoRepositoryImpl>();

        // Services
        services.AddScoped<ITodoService, TodoServiceImpl>();

        return services;
    }
}