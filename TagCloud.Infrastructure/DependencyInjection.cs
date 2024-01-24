using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITextLoader, FileTextLoader>();
        services.AddScoped<IImageStorage, FileImageStorage>();

        return services;
    }
}

