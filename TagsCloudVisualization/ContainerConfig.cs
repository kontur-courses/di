using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NHunspell;
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

        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            var executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.RegisterType<TextFileReader>().As<IFileReader>().AsSelf();
            builder.RegisterInstance(new Hunspell(executingPath + DictRuAff, executingPath + DictRuDic))
                .SingleInstance();
            builder.RegisterType<HunspellStemer>().As<IStemer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            builder.RegisterType<CustomFilter>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IEnumerable<string>),
                    (pi, ctx) => ctx.Resolve<TextFileReader>().ReadLines(executingPath + DictExcludeWords)))
                .As<IWordFilter>();
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
            builder.RegisterType<CanvasSettings>().As<ICanvasSettings>().SingleInstance();
            builder.RegisterType<TagStyleSettings>().As<ITagStyleSettings>().SingleInstance();
            builder.RegisterType<ImageWriter>().As<IImageWriter>();

            return builder.Build();
        }
    }
}