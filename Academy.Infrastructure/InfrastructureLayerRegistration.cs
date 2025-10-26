using Academy.Domain.Entities;
using Academy.Domain.Repositories;
using Academy.Infrastructure.DataContext;
using Academy.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Infrastructure;

public static class InfrastructureLayerRegistration
{
    public static IServiceCollection AddInfrastructureLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<DataInitializer>();

        services.AddScoped(typeof(IRepositoryAsync<>), typeof(EfCoreRepositoryAsync<>));
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddIdentityCore<AppUser>(x =>
        {
            x.Password.RequiredLength = 4;
            x.Password.RequireNonAlphanumeric = false;
            x.Password.RequireUppercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}
