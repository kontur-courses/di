using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.Visualization;
using TagCloud.WordsProcessing;
using Autofac;
using CommandLine;
using TagCloud.Algorithm;
using TagCloud.App;
using TagCloud.Visualization.WordPainting;

namespace TagCloud
{
    public class Program
    {
        private static void RegisterOptionIndependentDependencies(ContainerBuilder builder)
        {
            builder.RegisterInstance(new PictureConfig()).As<PictureConfig>();
            builder.RegisterType<TxtTextReader>().As<ITextReader>();
            builder.RegisterType<MicrosoftWordTextReader>().As<ITextReader>();
            builder.RegisterType<TextReaderSelector>().As<ITextReaderSelector>();
            builder.RegisterType<FileInfoProvider>().As<IFileInfoProvider>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<WordSizeSetter>().As<IWordSizeSetter>();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>();
            builder.RegisterType<WordClassBasedSelector>().As<IWordSelector>();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();
            builder.RegisterType<PngImageFormat>().As<IImageFormat>();
            builder.RegisterType<TagCloudGenerator>().As<ITagCloudGenerator>();
            builder.RegisterType<TagCloudElementsPreparer>().As<ITagCloudElementsPreparer>()
                .InstancePerLifetimeScope();
            builder.RegisterType<WordDrawer>().As<ITagCloudElementDrawer>();
            builder.RegisterType<Application>().AsSelf();
        }

        private static IWordPainter GetWordPainter(IComponentContext c)
        {
            var settings = c.Resolve<ISettingsProvider>().GetSettings();
            var config = c.Resolve<PictureConfig>();
            var painters = new List<IWordPainter>
            {
                new IndexBasedWordPainter(config),
                new RandomColorWordPainter(),
                new WordClassBasedWordPainter(config)
            };
            var painter = painters.First(p => p.Name == settings.WordPainterAlgorithmName);
            return painter;
        }

        private static void Execute(Options options)
        {
            var projectsDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var myStemPath = $"{projectsDirectory}/mystem.exe";

            var builder = new ContainerBuilder();
            RegisterOptionIndependentDependencies(builder);
            builder.RegisterInstance(new MyStemBasedWordClassIdentifier(myStemPath)).As<IWordClassIdentifier>();

            builder.Register(c =>
                    new ConsoleSettingsProvider(options, c.Resolve<PictureConfig>()))
                .As<ISettingsProvider>().InstancePerLifetimeScope();
            builder.Register(GetWordPainter).As<IWordPainter>()
                  .InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            var container = builder.Build();


            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Application>();
                try
                {
                    app.Run();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Execute)
                .WithNotParsed(errors => Console.WriteLine(string.Join("\\n", errors)));
        }
    }
}
