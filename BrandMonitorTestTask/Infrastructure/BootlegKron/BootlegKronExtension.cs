namespace BrandMonitorTestTask.Infrastructure.BootlegKron;

public static class BootlegKronExtension
{
    public static IServiceCollection AddBootlegKron(this IServiceCollection services)
    {
        services.AddHostedService<BootlegKronRunner>();
        return services;
    }
}