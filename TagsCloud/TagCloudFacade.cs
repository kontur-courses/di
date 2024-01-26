using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Builders;
using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.Extensions;
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
        var words = FileHelper
                    .GetLinesFromFile(filePath)
                    .Select(word => new WordToStatus { Word = word })
                    .ToList();

        StartFilterConveyor(words);
        var builder = new CloudTagListBuilder(options, words);

        return CloudTagCreator.CreateCloudTagList(builder);
    }

    public void GenerateTagCloudImage(List<CloudTag> cloudTags, string filename)
    {
        new VisualizationBuilder(options.ImageSize, options.BackgroundColor)
            .CreateImageFrom(cloudTags)
            .SaveAs(filename);
    }

    private void StartFilterConveyor(List<WordToStatus> words)
    {
        var provider = new ServiceCollection()
                       .AddFiltersWithOptions(options)
                       .BuildServiceProvider();

        var filterConveyor = provider.GetService<FilterConveyor>();
        filterConveyor!.ApplyFilters(words);
    }
}