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

            ConfigureCloudImageGenerator(builder);

            ConfigureApp(builder);

            return builder.Build();
        }

        private static void ConfigureFileReader(IAppConfig appConfig, ContainerBuilder builder)
        {
            builder.Register<IFileReader>(
               (c, p) =>
               {
                   var filePath = appConfig.inputTextFilePath;

                   if (filePath.Contains(".doc"))
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

        private static void ConfigureCloudLayouter(ContainerBuilder builder)
        {
            builder.Register(c => new CircularCloudLayouter(new EllipsePointGenerator(new Point(0, 0)))).As<ICloudLayouter>();
        }

        private static void ConfigureImageSettings(IAppConfig appConfig, ContainerBuilder builder)
        {
            builder.Register(с => appConfig.imageSettings).As<IImageSettings>();
            builder.Register(с => appConfig.imageSettings.WordColoring).As<IWordColoring>();
            builder.Register(с => appConfig.imageSettings.FontFamily).As<FontFamily>();
        }

        private static void ConfigureCloudImageGenerator(ContainerBuilder builder)
        {
            builder.RegisterType<CloudImageGenerator>().
                    UsingConstructor(typeof(ICloudLayouter), typeof(IImageSettings)).
                    As<ICloudImageGenerator>();
        }

        private static void ConfigureApp(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleApp>().As<IApp>();
        }
    }
}
