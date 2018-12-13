using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using TagsCloudContainer.Visualisation;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Formatting;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.TagsCloudGenerating;
using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();

            containerBuilder.RegisterType<CLI>().As<IUI>().WithParameter("args", args).SingleInstance();
            containerBuilder.RegisterType<DocWordsReader>().As<IWordsReader>().SingleInstance();

            containerBuilder.RegisterType<FormattingSettings>().AsSelf()
                .UsingConstructor(typeof(IUI), typeof(IFormattersFactory));
            containerBuilder.RegisterType<FormattingComponent>().AsSelf().SingleInstance()
                .UsingConstructor(typeof(FormattingSettings));

            containerBuilder.RegisterType<FilteringComponent>().AsSelf().SingleInstance()
                .UsingConstructor(typeof(FilteringSettings));
            containerBuilder.RegisterType<FilteringSettings>().AsSelf()
                .UsingConstructor(typeof(IUI), typeof(IFiltersFactory));


            containerBuilder.RegisterType<TagsCloudGeneratorSettings>()
                .UsingConstructor
                    (typeof(IUI), typeof(ITagsCloudLayouter), typeof(IWordsSizer))
                .SingleInstance();
            containerBuilder.RegisterType<TagsCloudGenerator>().AsSelf()
                .UsingConstructor(typeof(TagsCloudGeneratorSettings)).SingleInstance();

            containerBuilder.RegisterType<TagsCloudLayouterSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>()
                .UsingConstructor(typeof(TagsCloudLayouterSettings), typeof(ITagsCloudFactory)).SingleInstance();


            containerBuilder.RegisterType<ImageSettings>().AsSelf().UsingConstructor(typeof(IUI)).SingleInstance();

            containerBuilder.RegisterType<PngTagsCloudRenderer>().As<ITagsCloudRenderer>()
                .UsingConstructor(typeof(ImageSettings)).SingleInstance();


            containerBuilder.RegisterType<TagsCloudContainerApplication>().AsSelf().SingleInstance();
            var containerApplication = containerBuilder.Build();
            var app = containerApplication.Resolve<TagsCloudContainerApplication>();
            app.Run();
        }
    }
}