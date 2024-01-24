using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.Extensions;
using TagsCloud.Factories;
using TagsCloud.Helpers;
using TagsCloudVisualization;

namespace TagsCloud;

public class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    private void OnExecute()
    {
        // TODO: form this options from user input
        var filterOptions = new FilterOptions(
            CaseType.Lower,
            true,
            new List<string> { "S" },
            new List<string>());

        var fontCollection = new FontCollection();
        fontCollection.Add("/home/luvairo/Desktop/Roboto-Medium.ttf");
        var fontFamily = fontCollection.Families.First();

        var colorizer = ColorizerHelper.GetAppropriateColorizer(
            new[] { Color.Red, Color.Green }, "onevsrest")!;

        var factoryOptions = new CloudTagFactoryOptions(
            fontFamily,
            colorizer,
            new LayoutOptions(new Spiral(0.1f, (float)Math.PI / 180),
                new PointF((float)1920 / 2, (float)1080 / 2)));

        var provider = new ServiceCollection()
            .AddFiltersWithOptions(filterOptions)
            .BuildServiceProvider();

        var lines = FileHelper.GetLinesFromFile("/home/luvairo/textdata.txt");
        provider.GetRequiredService<FilterConveyor>().ApplyFilters(lines);

        var cloudTags = CloudTagCreator.CreateCloudTagList(new CloudTagFactory(factoryOptions, lines));
        
        new VisualizationBuilder(new Size(1920, 1080), Color.White)
            .CreateImageFrom(cloudTags)
            .SaveAs("image.png");

        Console.WriteLine();
    }
}