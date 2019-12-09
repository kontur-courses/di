using System.Drawing;
using Autofac;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.TagCloudLayouters;
using TagsCloudVisualization.TagsCloudVisualization;
using TagsCloudVisualization.TextRenderers;
using TagsCloudVisualization.WordAnalyzers.YandexAnalyzer;

namespace TagsCloudVisualization
{
    internal class TagCloud
    { 
        public static void Main(string[] args)
        {
            var builtContainer = GenerateEnvironment(args);
            var frequencyDictionary = builtContainer.Resolve<TextHandler>().GetFrequencyDictionary();
            var cloudGenerator = builtContainer.Resolve<ITagsCloudVisualization<Rectangle>>();
            cloudGenerator.Draw(frequencyDictionary);
        }

        private static IContainer GenerateEnvironment(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ArgumentParser>().As<IArgumentParser>();
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateImageSettings(args, new DefaultRenderer()));
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateCloudSettings(args));
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateTextSettings(args, new YandexStemmer()));
            containerBuilder.RegisterType<CircularCloudLayouter>().As(typeof(ILayouter));
            containerBuilder.RegisterType<TextHandler>().As(typeof(TextHandler));
            containerBuilder.RegisterType<WordsCloudVisualization>().As(typeof(ITagsCloudVisualization<Rectangle>));
            return containerBuilder.Build();
        }
    }
}