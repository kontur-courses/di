using System.Drawing;
using System.Reflection;
using Autofac;
using DeepMorphy;
using TagsCloudContainer;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.Visualizer.ColorGenerators;
using TagsCloudContainer.Visualizer.VisualizerSettings;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloud.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var appSettings = AppSettings.Parse(args);
            var tagCloudSettings = TagCloudSettings.Parse(args);
            using var container = Configure(appSettings, tagCloudSettings);
            container.Resolve<IConsoleUI>()
                .Run(appSettings, tagCloudSettings);
        }

        internal static IContainer Configure(IAppSettings settings, ITagCloudSettings tagCloudSettings)
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetAssembly(typeof(TagCloud));
            builder.RegisterInstance(settings).As<IAppSettings>().SingleInstance();
            builder.RegisterInstance(tagCloudSettings).As<ITagCloudSettings>().SingleInstance();
            builder.RegisterAssemblyTypes(assembly!).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => c.Resolve<IFactory<IVisualizerSettings>>().Create())
                .As<IVisualizerSettings>()
                .SingleInstance();
            builder.Register(c => c.Resolve<IFactory<IPointsProvider>>().Create()).As<IPointsProvider>();
            builder.RegisterType<ConsoleUI>().AsImplementedInterfaces();
            builder.RegisterInstance(new MorphAnalyzer()).SingleInstance();
            return builder.Build();
        }
    }
}