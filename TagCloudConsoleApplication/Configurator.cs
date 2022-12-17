using System.Drawing;
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
            .WithParameter("pathToMyStem", GetPathToMyStam());

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

    private void RegisterWordColoring(ContainerBuilder builder, TagCloudOptions options)
    {
        var coloring = options.WordColoring.ToLower();
        if (coloring == "white")
            builder.RegisterType<WhiteWordColoring>().As<IWordColoring>();
        else if (coloring == "rainbow")
            builder.RegisterType<RainbowWordColoring>().As<IWordColoring>();
        else
            throw new ArgumentException();
    }

    private void RegisterFileReader(ContainerBuilder builder, TagCloudOptions options)
    {
        if (options.InputPath.Contains(".docx"))
            builder.RegisterType<DocReader>().As<IFileReader>();
        else if (options.InputPath.Contains(".txt"))
            builder.RegisterType<TxtReader>().As<IFileReader>();
        else if (options.InputPath.Contains(".pdf"))
            builder.RegisterType<PdfFileReader>().As<IFileReader>();
        else
            throw new ArgumentException("input is in an unsupported format");
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
            BackgroundColor = FromColor(options.BackgroundColor),
            Font = new Font(options.FontFamily, options.FontSize, FontStyle.Bold, GraphicsUnit.Point),
            Size = new Size(options.ImageWidth, options.ImageHeight)
        };
    }

    private static Color FromColor(ConsoleColor c)
    {
        var cInt = (int)c;

        var brightnessCoefficient = (cInt & 8) > 0 ? 2 : 1;
        var r = (cInt & 4) > 0 ? 64 * brightnessCoefficient : 0;
        var g = (cInt & 2) > 0 ? 64 * brightnessCoefficient : 0;
        var b = (cInt & 1) > 0 ? 64 * brightnessCoefficient : 0;

        return Color.FromArgb(r, g, b);
    }

    private static string GetPathToMyStam()
    {
        var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        return Path.Combine(directoryInfo.Parent.Parent.Parent.Parent.FullName +
                            "\\TagCloudPainter\\Lemmaizers\\mystem.exe");
    }
}