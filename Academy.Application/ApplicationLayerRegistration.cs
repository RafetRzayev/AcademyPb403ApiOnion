using Academy.Application.Dtos.AuthenticationDtos;
using Academy.Application.Mapping;
using Academy.Application.Services.Interfaces;
using Academy.Application.Services.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Application;

public static class ApplicationLayerRegistration
{
    public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(x => x.AddProfile<MappingProfile>());
 
        // Register generic CRUD service
        services.AddScoped(typeof(ICrudServiceAsync<,,,>), typeof(CrudManager<,,,>));
 
        // Register specific services
        services.AddScoped<IStudentService, StudentManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IImageService, ImageManager>();

        // Configure JWT settings
        services.Configure<JwtSettings>(options => 
        {
            options.Key = configuration.GetSection("JwtSettings").GetSection("Key").Value;
            options.Issuer = configuration.GetSection("JwtSettings").GetSection("Issuer").Value;
            options.Audience = configuration.GetSection("JwtSettings").GetSection("Audience").Value;
            if (int.TryParse(configuration.GetSection("JwtSettings").GetSection("DurationInMinutes").Value, out int duration))
            {
                options.DurationInMinutes = duration;
            }
        });

        return services;
    }
}
