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
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<WordSizeSetter>().As<IWordSizeSetter>();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<IndexBasedWordPainter>().As<IWordPainter>();
            builder.RegisterType<PngImageFormat>().As<IImageFormat>();
            builder.RegisterType<TagCloudGenerator>().As<ITagCloudGenerator>();
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
            builder
                .Register(c =>
                    new WordClassBasedSelector(c.Resolve<IWordClassIdentifier>(), excludedWordClasses))
                .As<IWordSelector>();
            builder.RegisterInstance(new MyStemBasedWordClassIdentifier(myStemPath)).As<IWordClassIdentifier>();
            builder.Register(c =>
                    new ConsoleSettingsProvider(options, c.Resolve<PictureConfig>()))
                .As<ISettingsProvider>();

            var container = builder.Build();


            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Application>();
                app.Run();
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
