using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.Extensions;
using TagsCloudVisualization;

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
                          .AddAllInjections()
                          .AddSingleton(inputOptions)
                          .AddSingleton(cloudOptions)
                          .AddSingleton(outputOptions)
                          .BuildServiceProvider();
    }

    public HashSet<WordTagGroup> GenerateTagCloud(string inputFile, string outputFile)
    {
        var textProcessor = serviceProvider.GetRequiredService<IInputProcessor>();
        var cloudProcessor = serviceProvider.GetRequiredService<ICloudProcessor>();
        var outputProcessor = serviceProvider.GetRequiredService<IOutputProcessor>();

        var groups = textProcessor.CollectWordGroupsFromFile(inputFile);

        cloudProcessor.SetFonts(groups);
        cloudProcessor.SetPositions(groups);
        cloudProcessor.SetColors(groups);

        outputProcessor.SaveVisualization(groups, outputFile);

        return groups;
    }
}