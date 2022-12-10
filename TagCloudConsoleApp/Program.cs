using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Autofac;
using CommandLine;
using TagCloud.CloudGenerator;
using TagCloud.ColoringAlgorithm;
using TagCloud.ColoringAlgorithm.Provider;
using TagCloud.ImageGenerator;
using TagCloud.LayoutAlgorithm;
using TagCloud.LayoutAlgorithm.CircularLayoutAlgorithm;
using TagCloud.Parser;
using TagCloud.Parser.ParsingConfig;
using TagCloud.Parser.ParsingConfig.MyStemParsingConfig;
using TagCloud.WordSizingAlgorithm;

namespace TagCloudConsoleApp;

public static class Program
{
    public static int Main(string[] args)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("App only supports OS Windows.");
            return -1;
        }

        var exitCode = Parser.Default.ParseArguments<Options>(args).MapResult(Run, HandleErrors);
        return exitCode;
    }
    
    private static int Run(Options options)
    {
        var container = ConfigureContainer(options);

        var generator = container.Resolve<ICloudGenerator>();
        var image = generator.GenerateCloud(options.InputFile);
        image.Save(options.OutputFile, ImageFormat.Png);
        Console.WriteLine($"Image saved as {options.OutputFile}");
        return 0;
    }

    private static IContainer ConfigureContainer(Options options)
    {
        var containerBuilder = new ContainerBuilder();
        RegisterLayoutAlgorithm(options, containerBuilder);
        RegisterWordSizingAlgorithm(options, containerBuilder);
        RegisterParsingConfig(containerBuilder);
        RegisterParser(options, containerBuilder);
        RegisterColoringAlgorithms(containerBuilder, options);
        RegisterImageGenerator(options, containerBuilder);
        RegisterCloudGenerator(containerBuilder);
        return containerBuilder.Build();
    }

    private static void RegisterCloudGenerator(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<TagCloudGenerator>().As<ICloudGenerator>();
    }

    private static void RegisterParser(Options options, ContainerBuilder containerBuilder)
    {
        if (options.InputFile.EndsWith(".docx"))
            containerBuilder.RegisterType<WordDocumentParser>().As<ITagParser>();
        else
            containerBuilder.RegisterType<PlainTextParser>().As<ITagParser>();
    }

    private static void RegisterImageGenerator(Options options, ContainerBuilder containerBuilder)
    {
        var imageGeneratorBuilder = containerBuilder.RegisterType<BitmapImageGenerator>().As<IImageGenerator>()
            .WithParameter(new TypedParameter(typeof(Size), new Size(options.Width, options.Height ?? options.Width)));
        imageGeneratorBuilder.WithParameter(options.FontName != null
            ? new TypedParameter(typeof(Font), new Font(options.FontName, 1))
            : new TypedParameter(typeof(Font), new Font("Arial", 1)));
    }

    private static void RegisterWordSizingAlgorithm(Options options, ContainerBuilder containerBuilder)
    {
        var sizingBuilder = containerBuilder.RegisterType<RelativeWordSizingAlgorithm>().As<IWordSizingAlgorithm>();
        if (options.FontSize != null)
            sizingBuilder.WithParameter(new TypedParameter(typeof(int), options.FontSize));
    }

    private static void RegisterParsingConfig(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<BoringWordsMyStemParsingConfig>().As<IParsingConfig>();
    }

    private static void RegisterLayoutAlgorithm(Options options, ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<CircularLayoutAlgorithm>().As<ILayoutAlgorithm>()
            .WithParameter(new TypedParameter(typeof(Point),
                new Point(options.Width / 2, options.Height != null ? options.Height.Value / 2 : options.Width / 2)));
    }

    private static void RegisterColoringAlgorithms(ContainerBuilder containerBuilder, Options options)
    {
        var expectedAlgorithm = options.ColoringAlgorithm;
        var algorithm = ColoringAlgorithmProvider.Algorithms
            .Single(t => t.Name.StartsWith(expectedAlgorithm));

        var registrationBuilder = containerBuilder.RegisterType(algorithm).As<IColoringAlgorithm>();

        if (options.ColorNames == null)
            return;
        
        var colorNames = options.ColorNames.ToList();
        var colors = colorNames.Select(Color.FromName).ToArray();
        if (colors.Length > 0)
            registrationBuilder
                .WithParameter(new TypedParameter(typeof(Color), colors[0]))
                .WithParameter(new TypedParameter(typeof(Color[]), colors));
        else
            registrationBuilder
                .WithParameter(new TypedParameter(typeof(Color), Color.Red))
                .WithParameter(new TypedParameter(typeof(Color[]), new[]
                {
                    Color.Red, Color.Orange, Color.Yellow, Color.Green,
                    Color.LightBlue, Color.Blue, Color.Purple
                }));

    }

    private static int HandleErrors(IEnumerable<Error> errors)
    {
        var result = -1;
        if (errors.Any(x => x is HelpRequestedError or VersionRequestedError))
            result = 0;
        return result;
    }
}