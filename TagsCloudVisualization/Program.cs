using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.CommandLine;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.PointCreators;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.TextHandlers;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudVisualization;

public class Program
{
    public static void Main(string[] args)
    {
        var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
        var builder = new ContainerBuilder();

        builder.RegisterType<FileSettings>().WithParameters(new[]
        {
            new NamedParameter("pathToBoringWords", options.PathToBoringWords),
            new NamedParameter("pathToText", options.PathToText),
            new NamedParameter("pathToSaveDirectory", options.PathToSaveDirectory),
            new NamedParameter("fileName", options.FileName),
            new NamedParameter("fileFormat", options.FileFormat)
        });
        builder.RegisterType<FontSettings>().WithParameters(new[]
        {
            new NamedParameter("font", options.Font),
            new NamedParameter("color", options.Color),
            new NamedParameter("minSize", options.MinFontSize),
            new NamedParameter("maxSize", options.MaxFontSize)
        });
        builder.RegisterType<ImageSettings>().WithParameters(new[]
        {
            new NamedParameter("width", options.ImageWidth),
            new NamedParameter("height", options.ImageHeight),
            new NamedParameter("backgroundColor", options.BackgroundColor)
        });
        builder.RegisterType<SpiralSettings>().WithParameters(new[]
        {
            new NamedParameter("deltaAngle", options.DeltaAngle),
            new NamedParameter("deltaRadius", options.DeltaRadius)
        });
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<TagsLayouter>().As<ITagLayouter>();
        builder.RegisterType<ColorGenerator>().As<IColorGenerator>();
        builder.RegisterType<FileReader>().As<IFileReader>();
        builder.RegisterType<ImageSaver>().As<IImageSaver>();
        builder.RegisterType<Spiral>().As<IPointCreator>();
        builder.RegisterType<TextHandler>().As<ITextHandler>();
        builder.RegisterType<Visualizer>().As<IVisualizer>();

        try 
        {
            var container = builder.Build();
            var tagsLayouter = container.Resolve<ITagLayouter>();
            var visualizer = container.Resolve<IVisualizer>();
            var imageSaver = container.Resolve<IImageSaver>();
            var tags = tagsLayouter.GetTags();
            var image = visualizer.Vizualize(tags);
            imageSaver.SaveImage(image);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}