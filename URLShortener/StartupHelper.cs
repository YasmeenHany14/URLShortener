using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using URLShortener.Infra;
using URLShortener.Infra.Helpers;
using URLShortener.Infra.Repositories;
using URLShortener.Services;

namespace URLShortener;

public static class StartupHelper
{ 
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateAuditFieldsInterceptor>();
        services.AddDbContext<UrlContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<UpdateAuditFieldsInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(auditInterceptor);
        });

        services.AddScoped<IUrlRepository, UrlRepository>();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IUrlShorteningService, UrlShorteningService>();
        services.AddScoped<ICacheUrlService, CacheUrlService>();
        return services;
    }
}
