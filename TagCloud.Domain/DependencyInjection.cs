using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain
        (this IServiceCollection services,
        DomainOptions options
        )
    {
        services.AddScoped<ITagCloud, TagCloud>();
        services.AddScoped<IWordExtractor, WordExtractor>();
        services.AddScoped<IFrequencyDictionaryBuilder<string>, FrequencyDictionaryBuilder<string>>();
        services.AddScoped<ITagCloudRenderer, TagCloudRenderer>();
        services.AddScoped<ITagCloudLayouter, TagCloudLayouter>();
        services.AddScoped<IColorProvider, OpacityColorProvider>();
        services.AddScoped<IWordsFilter, LengthFilter>();

        services.AddSingleton(options.TagCloudOptions);
        services.AddSingleton(options.RenderOptions);
        services.AddSingleton(options.WordExtractionOptions);

        return services;
    }
}

