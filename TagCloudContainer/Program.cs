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

        private static void InitPreprocessorContainer(Config config)
        {
            var builder = new ContainerBuilder();
            var rawText = "";
            var text = "";
            var allWords = new List<string>();
            var validWords = new List<string>();
            
            builder.RegisterType<XmlWordExcluder>().As<IWordExcluder>();
            builder.RegisterType<TxtFileReader>()
                .As<IFileReader>()
                .OnActivating(x => rawText = x.Instance.ReadFromFile(config.InputFile));
            builder.RegisterType<TxtReader>()
                .As<IReader>()
                .OnActivated(x => text = x.Instance.GetTextFromRawFormat(rawText));
            builder.RegisterType<TextParser>()
                .As<ITextParser>()
                .OnActivated(x => allWords = x.Instance.GetWords(text).ToList());
            builder.RegisterType<SimplePreprocessor>()
                .As<IPreprocessor>()
                .OnActivated(x => validWords = x.Instance.GetValidWords(allWords).ToList());

            builder.Register(x => allWords).Named<List<string>>("valid words");;
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
            
            InitPreprocessorContainer(config);
            InitLayouterContainer(config.Center);
            InitVisualisationContainer(config);
            
            var validWords = preprocessorContainer.ResolveKeyed<List<string>>("valid words");
            var vis = visualizationContainer.Resolve<ITagCloudVisualization>();

            vis.SaveTagCloud(
                config.FileName,
                config.OutPath,
                validWords);
        }
    }
}