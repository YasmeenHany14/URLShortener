using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Models;
using URLShortener.Infra;
using URLShortener.Infra.Helpers;

namespace URLShortener;

public static class StartupHelper
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditFieldsInterceptor>();
        services.AddSingleton<UserContext>();
        services.AddDbContext<UrlContext>((sp, options) =>
        {
            services.AddSingleton<UpdateAuditFieldsInterceptor>();
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
    public static WebApplication ConfigureServices(this WebApplication app)
    {
        return app;
    }
}
