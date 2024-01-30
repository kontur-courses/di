using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

[Injection(ServiceLifetime.Singleton)]
public class OutputProcessor : IOutputProcessor
{
    private readonly IOutputProcessorOptions outputOptions;

    public OutputProcessor(IOutputProcessorOptions outputOptions)
    {
        this.outputOptions = outputOptions;
    }

    public void SaveVisualization(HashSet<WordTagGroup> wordGroups, string filename)
    {
        new VisualizationBuilder(outputOptions.ImageSize, outputOptions.BackgroundColor)
            .CreateImageFrom(wordGroups)
            .SaveAs(filename, outputOptions.ImageEncoder);
    }
}