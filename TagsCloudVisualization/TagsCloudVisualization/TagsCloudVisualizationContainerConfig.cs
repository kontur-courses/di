using System;
using System.Linq;
using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public static class TagsCloudVisualizationContainerConfig
    {
        public static IContainer GetCompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TagsCloudApp>();
            builder.RegisterType<WordDataProvider>().As<IWordDataProvider>();
            builder.RegisterType<PointGeneratorDetector>().As<IPointGeneratorDetector>();
            builder.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            builder.Register(c => WordsExtractorSettingsProvider.GetDefaultSettings()).As<IWordsExtractorSettings>();
            builder.Register(c => PointGeneratorSettingsProvider.GetDefaultSettings()).As<IPointGeneratorSettings>();
            builder.RegisterType<WordsExtractor>().As<IWordsExtractor>();
            builder.RegisterType<WordsTransformer>().As<IWordsTransformer>();
            var pointGeneratorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IPointGenerator).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var type in pointGeneratorTypes)
                builder.RegisterType(type).As<IPointGenerator>();

            return builder.Build();
        }
    }
}
