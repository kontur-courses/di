using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
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
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<SimpleWordsValidator>().As<IWordsValidator>();
            builder.RegisterType<Preprocessor>().As<IPreprocessor>();
            
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
            var validWords = preprocessorContainer.Resolve<IPreprocessor>().GetValidWords(config.InputFile, config.Count).ToList();
            var vis = visualizationContainer.Resolve<ITagCloudVisualization>();

            vis.SaveTagCloud(
                config.FileName,
                config.OutPath,
                validWords);
        }
    }
}