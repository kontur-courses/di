using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using TagsCloudContainer;
using TagsCloudContainer.Export;
using TagsCloudContainer.Export.File;
using TagsCloudContainer.Load;
using TagsCloudContainer.Load.File;
using TagsCloudContainer.Processing;
using TagsCloudContainer.Render;
using TagsCloudContainer.Render.CircularCloud;

var app = new CommandLineApplication();
app.HelpOption();
var sourcePath = app.Option("-s|--source <FILE>", "Path to file with source data", CommandOptionType.SingleValue);
sourcePath.DefaultValue = "words.txt";
sourcePath.IsRequired().Accepts(builder => builder.LegalFilePath());
;

var exportFormat = app.Option("-f|--format <FORMAT>", "Format for export: png, jpeg", CommandOptionType.SingleValue);
exportFormat.DefaultValue = "png";
exportFormat.IsRequired().Accepts().Values("png", "jpeg");

var exportPath = app.Option("-e|--export <FILE>", "Path to export file", CommandOptionType.SingleValue);
exportPath.DefaultValue = "result.png";
exportPath.IsRequired().Accepts(builder => builder.LegalFilePath());

var jpegQuality = app.Option<int>("-q|--quality <QUALITY>", "Quality of JPEG export", CommandOptionType.SingleValue)
    .Accepts(o => o.Range(1, 100));
jpegQuality.DefaultValue = 95;

var backgroundColorName = app.Option("-bg|--background <COLOR>", "Background color", CommandOptionType.SingleValue);

var textColors = app.Option("-c|--color <COLOR>", "Text color", CommandOptionType.MultipleValue);

var textSize = app.Option<int>("-ts|--size <SIZE>", "Minimum text size", CommandOptionType.SingleValue);
textSize.DefaultValue = 20;

var fontName = app.Option("-tf|--font <FONTNAME>", "Text font name", CommandOptionType.SingleValue);
fontName.DefaultValue = "Arial";

app.OnExecuteAsync(async token =>
{
    var services = new ServiceCollection();
    var loaderOptions = new FileWordsLoaderOptions
    {
        FilePath = sourcePath.Value()
    };
    services.AddSingleton(loaderOptions);
    services.AddSingleton<IWordsLoader, FileWordsLoader>();

    var wordsProcessorOptions = new SimpleWordsProcessorOptions
    {
        // BoringWords = new HashSet<string> {"foo"}
    };
    services.AddSingleton(wordsProcessorOptions);
    services.AddSingleton<IWordsProcessor, SimpleWordsProcessor>();

    var renderOptions = new CircularCloudRenderOptions
    {
        // TextColor = Color.Blue,
    };
    if (!string.IsNullOrEmpty(backgroundColorName.Value()) &&
        Color.TryParse(backgroundColorName.Value(), out var backgroundColor))
    {
        renderOptions.BackgroundColor = backgroundColor;
    }

    if (textSize.ParsedValue > 0)
    {
        renderOptions.MinimumFontSize = textSize.ParsedValue;
    }

    var fontNameStr = fontName.Value();
    if (!string.IsNullOrEmpty(fontNameStr))
    {
        renderOptions.FontName = fontNameStr;
    }

    if (textColors.Values.Count > 0)
    {
        var colors = new HashSet<Color>();
        foreach (var colorStr in textColors.Values)
        {
            if (Color.TryParse(colorStr, out var textColor))
            {
                colors.Add(textColor);
            }
        }

        if (colors.Any())
        {
            renderOptions.TextColors = colors.ToArray();
        }
    }

    services.AddSingleton(renderOptions);
    services.AddSingleton<CircularCloudLayouter>();
    services.AddSingleton<ISpiral, LogarithmSpiral>();
    services.AddSingleton<ICloudRender, CircularCloudRender>();

    if (exportFormat.Value() == "png")
    {
        var exporterOptions = new PngFileCloudExporterOptions
        {
            FilePath = exportPath.Value()
        };
        services.AddSingleton(exporterOptions);
        services.AddSingleton<ICloudExporter, PngFileCloudExporter>();
    }
    else if (exportFormat.Value() == "jpeg")
    {
        var exporterOptions = new JpegFileCloudExporterOptions
        {
            FilePath = exportPath.Value()
        };
        if (jpegQuality.ParsedValue > 0)
        {
            exporterOptions.Quality = jpegQuality.ParsedValue;
        }

        services.AddSingleton(exporterOptions);
        services.AddSingleton<ICloudExporter, JpegFileCloudExporter>();
    }
    else
    {
        throw new InvalidOperationException($"Unknown export format {exportFormat.Value()}");
    }

    services.AddSingleton<TagsCloudGenerator>();

    var provider = services.BuildServiceProvider();

    var generator = provider.GetRequiredService<TagsCloudGenerator>();
    await generator.GenerateAndExportAsync(token);
});

await app.ExecuteAsync(args);