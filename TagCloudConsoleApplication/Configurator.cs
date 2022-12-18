using System.Drawing;
using System.Reflection;
using Autofac;
using Autofac.Core;
using TagCloudConsoleApplication.Options;
using TagCloudPainter.Builders;
using TagCloudPainter.Coloring;
using TagCloudPainter.Common;
using TagCloudPainter.FileReader;
using TagCloudPainter.Layouters;
using TagCloudPainter.Lemmaizers;
using TagCloudPainter.Painters;
using TagCloudPainter.Preprocessors;
using TagCloudPainter.Savers;
using TagCloudPainter.Sizers;

namespace TagCloudConsoleApplication;

public class Configurator
{
    public IContainer Confiugre(TagCloudOptions o)
    {
        var builder = new ContainerBuilder();

        var imageSettings = GetImageSettings(o);

        builder.RegisterType<ImageSettingsProvider>().As<IImageSettingsProvider>()
            .WithProperty("ImageSettings", imageSettings);

        builder.RegisterType<ParseSettingsProvider>().As<IParseSettingsProvider>()
            .WithProperty("ParseSettings", new ParseSettings()).SingleInstance();

        builder.RegisterType<Lemmaizer>().As<ILemmaizer>().SingleInstance()
            .WithParameter("pathToMyStem", GetPathToMyStem());

        RegisterFileReader(builder, o);

        builder.RegisterType<WordPreprocessor>().As<IWordPreprocessor>();

        builder.RegisterType<WordSizer>().As<IWordSizer>();

        RegisterLayouter(builder, o);

        RegisterWordColoring(builder, o);

        builder.RegisterType<CloudElementBuilder>().As<ITagCloudElementsBuilder>();

        builder.RegisterType<CloudPainter>().As<ICloudPainter>();

        builder.RegisterType<TagCloudSaver>().As<ITagCloudSaver>();

        return builder.Build();
    }

    private void RegisterWordColoring(ContainerBuilder builder, TagCloudOptions o)
    {
        builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloudPainter)))
            .Where(t => t.Name.ToLower().StartsWith(o.WordColoring.ToLower()) && t.Name.EndsWith("WordColoring"))
            .As<IWordColoring>();
    }

    private void RegisterFileReader(ContainerBuilder builder, TagCloudOptions o)
    {
        var format = GetInputFormat(o.InputPath);
        builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloudPainter)))
            .Where(t => t.Name.ToLower().StartsWith(format) && t.Name.EndsWith("Reader"))
            .As<IFileReader>();
    }

    private void RegisterLayouter(ContainerBuilder builder, TagCloudOptions o)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameters(
            new List<Parameter>
            {
                new NamedParameter("center", new Point(o.XCenter, o.YCenter)),
                new NamedParameter("angleStep", o.Angle),
                new NamedParameter("radiusStep", o.Radius)
            });
    }

    private static ImageSettings GetImageSettings(TagCloudOptions options)
    {
        return new ImageSettings
        {
            BackgroundColor = options.BackgroundColor,
            Font = new Font(options.FontFamily, options.FontSize, FontStyle.Bold, GraphicsUnit.Point),
            Size = new Size(options.ImageWidth, options.ImageHeight)
        };
    }

    private static string GetPathToMyStem()
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "Lemmaizers", "mystem.exe");
    }

    private static string GetInputFormat(string input)
    {
        var index = input.LastIndexOf('.');
        return input.Substring(index + 1);
    }
}