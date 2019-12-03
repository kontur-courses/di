using Autofac;
using TagsCloudVisualization.Handlers;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Stemmers;
using TagsCloudVisualization.TagCloudLayouters;
using TagsCloudVisualization.TagsCloudVisualization;

namespace TagsCloudVisualization
{
    internal class TagCloud
    { 
        public static void Main(string[] args)
        {
            var builtContainer = GenerateEnvironment(args);
            var cloudGenerator = builtContainer.Resolve<WordsCloudVisualization>();
            cloudGenerator.Draw();
        }

        private static IContainer GenerateEnvironment(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ArgumentParser>().As<IArgumentParser>();
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateImageSettings(args));
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateCloudSettings(args));
            containerBuilder.Register(container => container.Resolve<IArgumentParser>().CreateTextSettings(args));
            containerBuilder.RegisterType<YandexStemmer>().As(typeof(IStemmer));
            containerBuilder.RegisterType<CircularCloudLayouter>().As(typeof(CircularCloudLayouter));
            containerBuilder.RegisterType<TextHandler>().As(typeof(TextHandler));
            containerBuilder.Register(container => container.Resolve<TextHandler>().GetFrequencyDictionary());
            containerBuilder.RegisterType<WordsCloudVisualization>().As(typeof(WordsCloudVisualization));
            return containerBuilder.Build();
        }
    }
}