using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.Drawer;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.TagFactory;
using TagsCloudVisualization.TextProviders;
using TagsCloudVisualization.ToTagConverter;

namespace TagsCloudVisualization.CLI;

public static class Bootstrapper
{
    public static IServiceCollection AddTagCloudVisualization(this IServiceCollection services,
        TagsCloudVisualizationSettings settings)
    {
        services.AddSingleton<Visualizer>();
        services.AddScoped<ITagFactory, TagFactory.TagFactory>();
        services.AddScoped(_ => settings.CloudLayouter);
        services.AddScoped<IToTagConverter, WordToTagConverter>();
        services.AddScoped(_ => settings.ImageSettingsProvider);
        services.AddScoped(_ => settings.ColorGenerator);
        services.AddScoped(_ => settings.FontSettingsProvider);
        services.AddScoped(_ => GetTextProvider(settings.Filepath));
        services.AddScoped<IDrawer, ImageDrawer>();
        services.AddScoped(_ => settings.ImageSaver);
        services.AddPreprocessors(settings);
        return services;
    }

    private static ITextProvider GetTextProvider(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("File path not set");
        }

        var extension = Path.GetExtension(path).TrimStart('.');
        return extension switch
        {
            "txt"  => new TxtTextProvider(path),
            "doc"  => new DocTextProvider(path),
            _ => throw new Exception($"Not found text reader for *.{extension}")
        };
    }

    private static void AddPreprocessors(this IServiceCollection services, TagsCloudVisualizationSettings settings)
    {
        var preprocessors = new List<IPreprocessor>
        {
            new LowerCasePreprocessor(),
            new BoringPreprocessor(settings.BoringWords.ToList())
        };

        services.AddSingleton<IPreprocessor>(_ => new MultiPreprocessor(preprocessors));
    }
}