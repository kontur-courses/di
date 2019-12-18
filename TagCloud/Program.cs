using System;
using System.Collections.Generic;
using System.IO;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.Visualization;
using TagCloud.WordsProcessing;
using Autofac;
using CommandLine;
using TagCloud.Algorithm;
using TagCloud.App;

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
            builder.RegisterType<IndexBasedWordPainter>().As<IWordPainter>();
            builder.RegisterType<PngImageFormat>().As<IImageFormat>();
            builder.RegisterType<TagCloudGenerator>().As<ITagCloudGenerator>();
            builder.RegisterType<TagCloudElementsPreparer>().As<ITagCloudElementsPreparer>();
            builder.RegisterType<WordPainter>().As<ITagCloudElementPainter>();
            builder.RegisterType<Application>().AsSelf();
        }

        private static void Execute(Options options)
        {
            var projectsDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var myStemPath = $"{projectsDirectory}/mystem.exe";
            var excludedWordClasses = new HashSet<WordClass>
                {WordClass.Conjunction, WordClass.Preposition, WordClass.Particle, WordClass.Pronoun};
            
            var builder = new ContainerBuilder();
            RegisterOptionIndependentDependencies(builder);
            builder.RegisterInstance(new MyStemBasedWordClassIdentifier(myStemPath)).As<IWordClassIdentifier>();


            builder.Register(c =>
                    new ConsoleSettingsProvider(options, c.Resolve<PictureConfig>()))
                .As<ISettingsProvider>();

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
