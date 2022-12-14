using System;
using System.Drawing;
using System.Linq;
using TagCloud.FileReader;
using TagCloud.FrequencyAnalyzer;
using TagCloud.WordColoring;
using TagCloud.ImageProcessing;
using TagCloud.PointGenerator;
using TagCloud.TextParsing;
using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;
using System.Reflection;
using TagCloud.CloudLayouter;

namespace TagCloud
{
    public static class ContainerConfig
    {
        public static IContainer Configure(IAppConfig appConfig)
        {
            var builder = new ContainerBuilder();

            ConfigureFileReader(appConfig, builder);

            ConfigureTextParser(builder);

            ConfigureWordsConvertering(builder);

            ConfigureWordsFiltering(builder);

            ConfigureWordsFrequencyAnalyzer(builder);

            ConfigureCloudLayouter(builder);

            ConfigureImageSettings(appConfig, builder);

            ConfigureWordColoring(appConfig, builder);

            ConfigurePointGenerator(appConfig, builder);

            ConfigureCloudImageGenerator(builder);

            ConfigureApp(builder);

            return builder.Build();
        }

        private static void ConfigureFileReader(IAppConfig appConfig, ContainerBuilder builder)
        {
            builder.Register<IFileReader>(
               (c, p) =>
               {
                   var filePath = appConfig.InputTextFilePath;

                   var formats = Enum.GetNames(typeof(ValidInputFileFormats));

                   if (!formats.Any(t => filePath.Contains(t)))
                       throw new ArgumentException($"Input file <{filePath}> has invalid format");

                   if (filePath.Contains(".docx") && filePath.Contains(".doc"))
                       return new DocxFileReader();

                   return new TxtFileReader();
               });
        }

        private static void ConfigureTextParser(ContainerBuilder builder)
        {
            builder.RegisterType<TextParser>().As<ITextParser>();
        }

        private static void ConfigureWordsConvertering(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloud)))
                .Where(t => t.Namespace.Contains("WordConverter"))
                .AsImplementedInterfaces();
        }

        private static void ConfigureWordsFiltering(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloud)))
                .Where(t => t.Namespace.Contains("WordFilter"))
                .AsImplementedInterfaces();
        }

        private static void ConfigureWordsFrequencyAnalyzer(ContainerBuilder builder)
        {
            builder.RegisterType<WordsFrequencyAnalyzer>().As<IWordsFrequencyAnalyzer>();
        }

        private static void ConfigurePointGenerator(IAppConfig appConfig, ContainerBuilder builder)
        {
            builder.Register(c => appConfig.CloudCentralPoint).As<Point>();

            switch (appConfig.CloudForm.ToLower())
            {
                case "circle":
                    builder.RegisterType<CirclePointGenerator>().As<IPointGenerator>();
                    break;
                case "ellipse":
                    builder.RegisterType<EllipsePointGenerator>().As<IPointGenerator>();
                    break;
                default:
                    throw new ArgumentException($"Cloud form <{appConfig.CloudForm}> isn't supported");
            }
        }

        private static void ConfigureCloudLayouter(ContainerBuilder builder)
        {
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
        }

        private static void ConfigureImageSettings(IAppConfig appConfig, ContainerBuilder builder)
        {
            builder.Register(с => appConfig.ImageSettings).As<IImageSettings>();
            builder.Register(с => appConfig.ImageSettings.FontFamily).As<FontFamily>();
        }

        private static void ConfigureWordColoring(IAppConfig appConfig, ContainerBuilder builder)
        {
            IWordColoring coloring;

            switch (appConfig.ImageSettings.WordColoringAlgorithmName)
            {
                case "random":
                    coloring = new RandomColoring();
                    break;
                case "gradient":
                    coloring = new GradientColoring();
                    break;
                case "black":
                    coloring = new BlackColoring();
                    break;
                default:
                    throw new ArgumentException($"Word coloring <{appConfig.ImageSettings.WordColoringAlgorithmName}> isn't supported");
            }

            builder.Register(c => coloring).As<IWordColoring>();
        }

        private static void ConfigureCloudImageGenerator(ContainerBuilder builder)
        {
            builder.RegisterType<CloudImageGenerator>().
                    UsingConstructor(typeof(ICloudLayouter), typeof(IImageSettings), typeof(IWordColoring)).
                    As<ICloudImageGenerator>();
        }

        private static void ConfigureApp(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleApp>().As<IApp>();
        }
    }
}
