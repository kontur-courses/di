using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

public class OutputProcessor
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
            .SaveAs(filename);
    }
}