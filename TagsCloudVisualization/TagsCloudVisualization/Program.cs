using System;
using System.Linq;
using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
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
            builder.Register(c => WordsExtractorSettingsProvider.GetDefaultSettings()).As<IWordsExtractorSettings>();
            builder.Register(c => PointGeneratorSettingsProvider.GetDefaultSettings()).As<IPointGeneratorSettings>();
            builder.RegisterType<WordsExtractor>().As<IWordsExtractor>();
            var pointGeneratorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IPointGenerator).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var type in pointGeneratorTypes)
                builder.RegisterType(type).Named<IPointGenerator>(type.Name.ToLowerInvariant());

            return builder.Build();
        }
    }
}