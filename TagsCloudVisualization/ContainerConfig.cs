using System.Drawing;
using System.Linq;
using Autofac;
using NHunspell;
using TagsCloudVisualization.Common.FileReaders;
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
        public static IContainer ConfigureContainer(CommandLineOptions options)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<TextFileReader>().As<IFileReader>();
            const string dictRuAff = @"dicts\ru.aff";
            const string dictRuDic = @"dicts\ru.dic";
            builder.RegisterInstance(new Hunspell(dictRuAff, dictRuDic)).SingleInstance();
            builder.RegisterType<HunspellStemer>().As<IStemer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            const string defExcludeWords = @"filters\excludeWords.txt";
            builder.RegisterType<CustomFilter>()
                .As<IWordFilter>()
                .OnActivated(service => service.Instance.Load(defExcludeWords));
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>();
            builder.RegisterType<TagBuilder>().As<ITagBuilder>();
            builder.RegisterType<StyledTagBuilder>().As<IStyledTagBuilder>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<TagCloudPainter>().As<ITagCloudPainter>();
            builder.RegisterInstance(new CanvasSettings(
                    options.Width, options.Height,
                    Color.FromName(options.BackgroundColor.Trim())))
                .As<ICanvasSettings>().SingleInstance();
            builder.RegisterInstance(new TagStyleSettings(
                    options.ForegroundColors.Select(color => Color.FromName(color.Trim())).ToArray(),
                    options.Fonts.Select(font => font.Trim()).ToArray(),
                    options.TagSize,
                    options.TagSizeScatter))
                .As<ITagStyleSettings>().SingleInstance();
            return builder.Build();
        }
    }
}