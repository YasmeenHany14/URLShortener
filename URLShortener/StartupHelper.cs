using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using URLShortener.Infra;
using URLShortener.Infra.Helpers;
using URLShortener.Interfaces.Helpers;
using URLShortener.Interfaces.Services;
using URLShortener.Models;
using URLShortener.Services.AuthServices;

namespace URLShortener;

public static class StartupHelper
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditFieldsInterceptor>();
        services.AddSingleton<IUserContext, UserContext>();
        services.AddDbContext<UrlContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<UpdateAuditFieldsInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(auditInterceptor);
        });
        services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings
                options.User.RequireUniqueEmail = true; 
            }).AddEntityFrameworkStores<UrlContext>()
            .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(StartupHelper).Assembly);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        return services;
    }
}
