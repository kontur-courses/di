using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using Autofac.Core;
using FluentAssertions;
using TagsCloudContainer.Visualisation;
using NHunspell;
using TagsCloudContainer.Filters;
using TagsCloudContainer.Formatters;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Reading;
using TagsCloudContainer.UI;
using TagsCloudContainer.Weighting;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CLI>().As<IUI>().WithParameter("args", args).SingleInstance();
            containerBuilder.RegisterType<TxtWordsReader>().As<IWordsReader>().SingleInstance();

            containerBuilder.RegisterType<BlackListFilterSettings>().AsSelf();

            containerBuilder.RegisterType<BlacklistWordsFilter>().As<IWordsFilter>().SingleInstance()
                .UsingConstructor(typeof(BlackListFilterSettings));

            containerBuilder.RegisterType<ToInitFormFormatter>().As<IWordsFormatter>();
            containerBuilder.RegisterType<FrequencyWordsWeighter>().As<IWordsWeighter>().SingleInstance();

            containerBuilder.RegisterType<TagsCloudGeneratorSettings>()
                .UsingConstructor
                (typeof(IUI), typeof(IWordsFormatter), typeof(IWordsFilter), typeof(ITagsCloudLayouter),
                    typeof(IWordsWeighter))
                .SingleInstance();
            containerBuilder.RegisterType<TagsCloudGenerator>().AsSelf()
                .UsingConstructor(typeof(TagsCloudGeneratorSettings)).SingleInstance();

            containerBuilder.RegisterType<TagsCloudLayouterSettings>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>()
                .UsingConstructor(typeof(TagsCloudLayouterSettings)).SingleInstance();


            containerBuilder.RegisterType<ImageSettings>().AsSelf().UsingConstructor(typeof(IUI)).SingleInstance();

            containerBuilder.RegisterType<PNGTagsCloudRenderer>().As<ITagsCloudRenderer>()
                .UsingConstructor(typeof(ImageSettings)).SingleInstance();
            containerBuilder.RegisterType<TagsCloudContainerApplication>().AsSelf().SingleInstance();
            var containerApplication = containerBuilder.Build();
            var app = containerApplication.Resolve<TagsCloudContainerApplication>();
            app.Run();
        }
    }
}