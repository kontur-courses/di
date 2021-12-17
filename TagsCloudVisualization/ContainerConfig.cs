﻿using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NHunspell;
using TagsCloudVisualization.Commands;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.ImageWriters;
using TagsCloudVisualization.Common.Layouters;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Common.Stemers;
using TagsCloudVisualization.Common.TagCloudPainters;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;
using TagsCloudVisualization.Common.WordFilters;

namespace TagsCloudVisualization
{
    public static class ContainerConfig
    {
        private const string DictRuAff = @"\dicts\ru.aff";
        private const string DictRuDic = @"\dicts\ru.dic";
        private const string DictExcludeWords = @"\filters\excludeWords.txt";

        public static IContainer ConfigureContainer(CommandLineOptions options)
        {
            var builder = new ContainerBuilder();
            var executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.RegisterType<TextFileReader>().As<IFileReader>();
            builder.RegisterInstance(new Hunspell(executingPath + DictRuAff, executingPath + DictRuDic))
                .SingleInstance();
            builder.RegisterType<HunspellStemer>().As<IStemer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            builder.RegisterType<CustomFilter>()
                .As<IWordFilter>()
                .OnActivated(service => service.Instance.Load(executingPath + DictExcludeWords));
            builder.RegisterType<ComposeFilter>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWordFilter[]),
                    (pi, ctx) => ctx.Resolve<IWordFilter[]>()));
            builder.RegisterType<TextAnalyzer>()
                .As<ITextAnalyzer>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWordFilter),
                    (pi, ctx) => ctx.Resolve<ComposeFilter>()));
            builder.RegisterType<TagBuilder>().As<ITagBuilder>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<TagCloudPainter>().As<ITagCloudPainter>();
            builder.RegisterInstance(new CanvasSettings(options))
                .As<ICanvasSettings>().SingleInstance();
            builder.RegisterInstance(new TagStyleSettings(options))
                .As<ITagStyleSettings>().SingleInstance();
            builder.RegisterType<ImageWriter>().As<IImageWriter>();

            return builder.Build();
        }
    }
}