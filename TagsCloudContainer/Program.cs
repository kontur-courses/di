using System.Reflection;
using Autofac;
using FluentAssertions;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.UI;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var dataAccess = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();
            containerBuilder.RegisterType<CLI>().As<IUI>().SingleInstance();
            containerBuilder.RegisterType<DocWordsReader>().As<IWordsReader>();
            containerBuilder.RegisterType<FormattingComponent>();
            containerBuilder.RegisterType<FilteringComponent>();
            containerBuilder.RegisterType<TagsCloudGenerator>();
            containerBuilder.RegisterType<RandomColorManager>().As<IColorManager>();
            containerBuilder.RegisterType<BoringWordsRepository>().As<IBoringWordsRepository>().SingleInstance();
            containerBuilder.RegisterType<TagsCloudContainerApplication>();
            var containerApplication = containerBuilder.Build();
            var app = containerApplication.Resolve<TagsCloudContainerApplication>();
            app.Run(args);
        }
    }
}