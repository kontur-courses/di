using Autofac;
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

public static class DiContainerBuilder
{
    public static IContainer BuildContainer(CommandLineOptions options)
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<BackgroundSettings>().WithParameters(new[]
        {
            new NamedParameter("backgroundColor", options.BackgroundColor)
        });

        builder.RegisterType<ColorGeneratorSettings>().WithParameters(new[]
        {
            new NamedParameter("color", options.Color)
        });

        builder.RegisterType<ImageSaverSettings>().WithParameters(new[]
        {
            new NamedParameter("pathToSaveDirectory", options.PathToSaveDirectory),
            new NamedParameter("fileName", options.FileName),
            new NamedParameter("fileFormat", options.FileFormat)
        });

        builder.RegisterType<ImageSettings>().WithParameters(new[]
        {
            new NamedParameter("width", options.ImageWidth),
            new NamedParameter("height", options.ImageHeight)
        });

        builder.RegisterType<SpiralSettings>().WithParameters(new[]
        {
            new NamedParameter("deltaAngle", options.DeltaAngle),
            new NamedParameter("deltaRadius", options.DeltaRadius)
        });

        builder.RegisterType<TagsLayouterSettings>().WithParameters(new[]
        {
            new NamedParameter("font", options.Font),
            new NamedParameter("minSize", options.MinFontSize),
            new NamedParameter("maxSize", options.MaxFontSize)
        });

        builder.RegisterType<TextHandlerSettings>().WithParameters(new[]
        {
            new NamedParameter("pathToBoringWords", options.PathToBoringWords),
            new NamedParameter("pathToText", options.PathToText),
        });

        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<TagsLayouter>().As<ITagLayouter>();
        builder.RegisterType<RandomColorGenerator>().As<IColorGenerator>();
        builder.RegisterType<DefaultColorGenerator>().As<IColorGenerator>();
        builder.RegisterType<TxtReader>().As<IFileReader>();
        builder.RegisterType<DocReader>().As<IFileReader>();
        builder.RegisterType<DocxReader>().As<IFileReader>();
        builder.RegisterType<ImageSaver>().As<IImageSaver>();
        builder.RegisterType<Spiral>().As<IPointCreator>();
        builder.RegisterType<TextHandler>().As<ITextHandler>();
        builder.RegisterType<Visualizer>().As<IVisualizer>();
        builder.RegisterType<TagCloudCreator>();

        return builder.Build();
    }
}