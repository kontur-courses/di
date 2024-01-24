using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Extensions;
using TagsCloud.Factories;
using TagsCloud.Helpers;
using TagsCloudVisualization;

namespace TagsCloud;

public class TagCloudFacade
{
    private readonly ITagCloudOptions options;

    public TagCloudFacade(ITagCloudOptions options)
    {
        this.options = options;
    }

    public List<CloudTag> GenerateCloudTagList(string filePath)
    {
        var lines = FileHelper.GetLinesFromFile(filePath);
        StartFilterConveyor(lines);

        var factory = new CloudTagFactory(options, lines);

        return CloudTagCreator.CreateCloudTagList(factory);
    }

    public void GenerateTagCloudImage(List<CloudTag> cloudTags, string filename)
    {
        new VisualizationBuilder(options.ImageSize, options.BackgroundColor)
            .CreateImageFrom(cloudTags)
            .SaveAs(filename);
    }

    private void StartFilterConveyor(List<string> words)
    {
        var provider = new ServiceCollection()
            .AddFiltersWithOptions(options)
            .BuildServiceProvider();

        var filterConveyor = provider.GetService<FilterConveyor>();
        filterConveyor!.ApplyFilters(words);
    }
}