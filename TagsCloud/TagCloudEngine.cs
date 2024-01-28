using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.Extensions;
using TagsCloud.Formatters;
using TagsCloud.Processors;

namespace TagsCloud;

public class TagCloudEngine
{
    private readonly IServiceProvider serviceProvider;

    public TagCloudEngine(
        IInputProcessorOptions inputOptions,
        ICloudProcessorOptions cloudOptions,
        IOutputProcessorOptions outputOptions)
    {
        serviceProvider = new ServiceCollection()
                          .AddProcessors()
                          .AddReaders()
                          .AddFilters()
                          .AddPainters()
                          .AddMeasurers()
                          .AddSingleton(inputOptions)
                          .AddSingleton(cloudOptions)
                          .AddSingleton(outputOptions)
                          .AddSingleton<IPostFormatter, DefaultPostFormatter>()
                          .BuildServiceProvider();
    }

    public void GenerateTagCloud(string inputFile, string outputFile)
    {
        var textProcessor = serviceProvider.GetRequiredService<InputProcessor>();
        var cloudProcessor = serviceProvider.GetRequiredService<CloudProcessor>();
        var outputProcessor = serviceProvider.GetRequiredService<OutputProcessor>();

        var groups = textProcessor.CollectWordGroupsFromFile(inputFile);

        cloudProcessor.SetFonts(groups);
        cloudProcessor.SetPositions(groups);
        cloudProcessor.SetColors(groups);

        outputProcessor.SaveVisualization(groups, outputFile);
    }
}