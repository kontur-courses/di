using System;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudPreprocessor;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    class Program
    {
        private static IContainer container;

        private static void InitContainer(Point center)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleClient>().As<IUserInterface>();
            builder.Register(ctx => new SimplePreprocessor(
                new XmlWordExcluder(Environment.CurrentDirectory + "\\..\\..\\.."))
            ).As<IPreprocessor>();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();

            builder.Register(ctx => new ArchimedesSpiral(center)).As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            container = builder.Build();
        }

        static void Main(string[] args)
        {
            var client = new ConsoleClient();
            var config = client.GetConfig(args, out var toExit);
            if (toExit)
                Environment.Exit(0);
            InitContainer(config.Center);

            var layouter = container.Resolve<ICloudLayouter>();
            var preprocessor = container.Resolve<IPreprocessor>();
            var rawText = container.Resolve<IFileReader>().ReadFromFile(config.InputFile);
            var text = container.Resolve<IReader>().GetTextFromRawFormat(rawText);
            var allWords = container.Resolve<ITextParser>().GetWords(text);
            var validWords = preprocessor
                .GetValidWords(allWords)
                .Take(config.Count)
                .ToList();
            
            
            var vis = new TagCloudVisualization(
                layouter, 
                config.Font, 
                config.Color,
                Color.FromArgb(128, config.BackgroundColor));
            
            vis.SaveTagCloud(
                config.FileName, 
                config.OutPath, 
                validWords);
        }
    }
}