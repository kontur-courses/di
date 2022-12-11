using System.Drawing;
using System.Linq;
using TagCloud.FileReader;
using TagCloud.FrequencyAnalyzer;
using TagCloud.WordColoring;
using TagCloud.ImageProcessing;
using TagCloud.CloudLayouter;
using TagCloud.TextParsing;
using Autofac;
using TagCloud.AppConfig;
using TagCloud.App;
using System.Reflection;

namespace TagCloud
{
    public static class ContainerConfig
    {
        public static IContainer Configure(IAppConfig appConfig)
        {
            var builder = new ContainerBuilder();

            builder.Register<IFileReader>(
               (c, p) =>
               {
                   var filePath = appConfig.inputTextFilePath;

                   if (filePath.Contains(".doc"))
                       return new DocxFileReader();

                   return new TxtFileReader();
               });

            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloud)))
                .Where(t => t.Namespace.Contains("WordConverter"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(TagCloud)))
                .Where(t => t.Namespace.Contains("WordFilter"))
                .AsImplementedInterfaces();

            builder.RegisterType<WordsFrequencyAnalyzer>().As<IWordsFrequencyAnalyzer>();

            builder.Register(c => new CircularCloudLayouter(new Point(0, 0))).As<ICloudLayouter>();

            builder.Register(с => appConfig.imageSettings).As<IImageSettings>();
            builder.Register(с => appConfig.imageSettings.WordColoring).As<IWordColoring>(); 
            builder.Register(с => appConfig.imageSettings.FontFamily).As<FontFamily>();

            builder.RegisterType<CloudImageGenerator>().
                    UsingConstructor(typeof(ICloudLayouter), typeof(IImageSettings)).
                    As<ICloudImageGenerator>();

            builder.RegisterType<ConsoleApp>().As<IApp>();

            return builder.Build();
        }
    }
}
