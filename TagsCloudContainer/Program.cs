using System.Reflection;
using Autofac;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var dataAccess = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();
            containerBuilder.RegisterType<DocWordsReader>().As<IWordsReader>().SingleInstance();
            containerBuilder.RegisterType<BlacklistSettings>().AsSelf();
            containerBuilder.RegisterType<FormattingSettings>().AsSelf();
            containerBuilder.RegisterType<FormattingComponent>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<FilteringComponent>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<TagsCloudGeneratorSettings>();
            containerBuilder.RegisterType<TagsCloudGenerator>().AsSelf();
            containerBuilder.RegisterType<TagsCloudLayouterSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<RandomColorManager>().As<IColorManager>();
            containerBuilder.RegisterType<TagsCloudContainerApplication>().AsSelf().SingleInstance();
            var containerApplication = containerBuilder.Build();
            var app = containerApplication.Resolve<TagsCloudContainerApplication>();
            app.Run(args);
        }
    }
}