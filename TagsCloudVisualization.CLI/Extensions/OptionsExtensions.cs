using System.Drawing;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.CloudLayouter.PointGenerator;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.FontSettings;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.ImageSettings;

namespace TagsCloudVisualization.CLI.Extensions;

public static class OptionsExtensions
{
    public static TagsCloudVisualizationSettings GetVisualizationSettings(this Options options)
    {
        return new TagsCloudVisualizationSettings()
        {
            Filepath = Path.Combine(Options.DefaultDirectory,options.Filepath),
            OutputDirectory = options.OutputDirectory ?? Options.DefaultOutputDirectory,
            ImageSettingsProvider = new ImageSettingsProvider(
                Color.FromName(options.BackgroundColor),
                options.Width,
                options.Height),
            FontSettingsProvider = new FontSettingsProvider(options.FontSize, options.FontFamily),
            ColorGenerator = GetColorGenerator(options.ColorAlgorithm),
            ImageSaver = GetImageSaver(options.ImageFileExtension),
            CloudLayouter = GetCloudLayouterAlgorithm(options.LayoterAlgoritm),
            TagCount = options.TagCount
        };
    }

    private static IColorGenerator GetColorGenerator(string name)
    {
        return name switch
        {
            "rainbow" => new RainbowColorGenerator(new Random()),
            "random" => new RandomColorGenerator(new Random()),
            _ => throw new ArgumentException("Such color generator not supported")
        };
    }

    private static AbstractImageSaver GetImageSaver(string extension)
    {
        return extension switch
        {
            "jpeg" => new JpegImageSaver(),
            "png" => new PngImageSaver(),
            _ => throw new ArgumentException("Such image saver not supported")
        };
    }

    private static ICloudLayouter GetCloudLayouterAlgorithm(string name)
    {
        var point = Point.Empty;
        var algorithm = name switch
        {
            "circular" => new CircularCloudLayouter(new SpiralPointGenerator(point)),
            "random" => new CircularCloudLayouter(new RandomPointGenerator(new Random())),
            _ => throw new ArgumentException("Such layouter algorithm not supported")
        };
        return algorithm;
    }
}