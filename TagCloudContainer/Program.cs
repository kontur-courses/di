using System;
using System.Drawing;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using TagsCloudPreprocessor;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    class Program
    {
        private static IContainer preprocessorContainer;
        private static IContainer layouterContainer;
        private static IContainer visualizationContainer;

        private static void InitPreprocessorContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<XmlWordExcluder>().As<IWordExcluder>();
            builder.RegisterType<SimplePreprocessor>().As<IPreprocessor>();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            
            preprocessorContainer = builder.Build();
        }
        
        private static void InitLayouterContainer(Point center)
        {
            var builder = new ContainerBuilder();

            builder.Register(ctx => new ArchimedesSpiral(center)).As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
            
            layouterContainer = builder.Build();
        }
        
        private static void InitVisualisationContainer(Config config)
        {
            var builder = new ContainerBuilder();

            var layouter = layouterContainer.Resolve<ICloudLayouter>();
            
            builder.Register(ctx => 
                    new TagCloudVisualization(
                        layouter,
                        config.Font,
                        config.Color,
                        config.BackgroundColor
                    ))
                .As<ITagCloudVisualization>();
            
            visualizationContainer = builder.Build();
        }

        static void Main(string[] args)
        {
            var client = new ConsoleClient();
            var config = client.GetConfig(args, out var toExit);
            if (toExit)
                Environment.Exit(0);
            
            InitPreprocessorContainer();
            InitLayouterContainer(config.Center);
            InitVisualisationContainer(config);
            
            var preprocessor = preprocessorContainer.Resolve<IPreprocessor>();
            var rawText = preprocessorContainer.Resolve<IFileReader>().ReadFromFile(config.InputFile);
            var text = preprocessorContainer.Resolve<IReader>().GetTextFromRawFormat(rawText);
            var allWords = preprocessorContainer.Resolve<ITextParser>().GetWords(text);
            var validWords = preprocessor
                .GetValidWords(allWords)
                .Take(config.Count)
                .ToList();


            var vis = visualizationContainer.Resolve<ITagCloudVisualization>();

            vis.SaveTagCloud(
                config.FileName,
                config.OutPath,
                validWords);
        }
    }
}