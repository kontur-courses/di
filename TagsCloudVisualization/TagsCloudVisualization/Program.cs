using System;
using System.Linq;
using System.Reflection;
using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public interface IPipelineStep<T1, T2>{ }

    public class Program
    {
        private static void Main(string[] args)
        {
            var container = CompositionRoot();
            container.Resolve<TagsCloudApp>().Run(args, container);
        }

        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagsCloudApp>();
            builder.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            builder.RegisterType<WordDataProvider>().As<IWordDataProvider>();

            builder.RegisterType<SettingsManager>().OnActivating(e => e.Instance.Load())
                .As<IPointGeneratorSettingsProvider>().SingleInstance();
            builder.RegisterType<SettingsManager>().OnActivating(e => e.Instance.Load())
                .As<IWordsExtractorSettingsProvider>().SingleInstance();

            builder.RegisterType<WordsExtractorSettings>().As<IWordsExtractorSettingsProvider>();

            var pointGeneratorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IPointGenerator).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var type in pointGeneratorTypes)
                builder.RegisterType(type).Named<IPointGenerator>(type.Name.ToLowerInvariant());

            builder.RegisterType<WordsExtractor>().As<IWordsExtractor>();
            return builder.Build();
        }
    }
}