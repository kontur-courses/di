using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Features.AttributeFilters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Printing;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.WordProcessors;
using TagsCloudVisualization.WordValidators;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization
{
    public static class Configurator
    {
        private static void InjectParsers(ContainerBuilder builder, Config config)
        {
            builder.RegisterType<LinesParser>().As<LinesParser>().SingleInstance();
            
            switch (config.SourceTextInterpretationMode)
            {
                case SourceTextInterpretationMode.LiteraryText:
                    builder.RegisterType<LiteraryTextParser>().As<ITextParser>().SingleInstance();
                    builder.RegisterType<InitialFormWordProcessor>().As<IWordProcessor>().SingleInstance();
                    builder.Register(_ => WordList.CreateFromFiles(@"Russian.dic")).As<WordList>().SingleInstance();
                    break;
                case SourceTextInterpretationMode.OneWordPerLine:
                    builder.RegisterType<LinesParser>().As<ITextParser>().SingleInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config.SourceTextInterpretationMode));
            }
        }
        
        private static void InjectValidators(ContainerBuilder builder, Config config)
        {
            builder.Register(p =>
            {
                var format = TextFormat.GetFormatByExtension(Path.GetExtension(config.CustomIgnoreFilePath ?? config.DefaultIgnoreFilePath));
                var reader = p.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) ??
                             throw new ArgumentException($"no proper reader exist for {config.CustomIgnoreFilePath ?? config.DefaultIgnoreFilePath}");
                var words = reader.ReadFile(config.CustomIgnoreFilePath ?? config.DefaultIgnoreFilePath);
                return p.Resolve<LinesParser>().ParseText(words);
            }).Keyed<IEnumerable<string>>("IgnoreWords");
            
            builder.RegisterType<IgnoredWordsValidator>().As<IWordValidator>().WithAttributeFiltering();
            builder.Register(_ => new TooShortWordValidator(config.MinWordToStatisticLength)).As<IWordValidator>();
        }
        
        public static IContainer InjectWith(Config config)
        {
            var builder = new ContainerBuilder();
            
            builder.Register(_ => new PointSpiral(config.Center, config.Density, (int)config.Density)).As<IInfinityPointsEnumerable>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter<Rectangle>>();

            InjectProcessors(builder);
            InjectReaders(builder);
            InjectParsers(builder, config);
            InjectValidators(builder, config);
            
            builder.RegisterType<WordsStatistics>().As<IWordsStatistics>();
            builder.Register(_ => new DefaultWordStatisticsToSizeConverter(80)).As<IWordStatisticsToSizeConverter>();

            if (config.Color is null) builder.RegisterType<RandomColorScheme>().As<IColorScheme>();
            else builder.Register(_ => new SingleColorScheme(config.Color.Value)).As<IColorScheme>();
            
            builder.RegisterType<TextPrinter>().As<IPrinter<Text>>();
            builder.RegisterType<RectanglesReCalculator>().As<IRectanglesReCalculator>();

            return builder.Build();
        }

        private static void InjectProcessors(ContainerBuilder builder)
        {
            builder.RegisterType<WordsProcessor>().As<WordsProcessor>().SingleInstance();
            builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<WordsProcessor>().As<ITextProcessor>().SingleInstance();
        }

        private static void InjectReaders(ContainerBuilder builder)
        {
            builder.RegisterType<DocxReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<TxtReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<MyPdfReader>().As<IFileReader>().SingleInstance();
        }
        
        public static Bitmap CreateTags(Config config)
        {
            var container = InjectWith(config);
            var painter = container.BeginLifetimeScope().Resolve<IPrinter<Text>>();
            return painter.GetBitmap(GetRectangles(container, config), config.Size);
        }
        
        private static IEnumerable<Text> GetRectangles(ILifetimeScope container, Config config)
        {
            using var scope = container.BeginLifetimeScope();
            var layouter = scope.Resolve<ILayouter<Rectangle>>();
            var statistic = scope.Resolve<IWordsStatistics>();
            var format = TextFormat.GetFormatByExtension(Path.GetExtension(config.TextFilePath));
            var reader = scope.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) ?? throw new ArgumentException("no proper reader exist");
            var text = scope.Resolve<ITextProcessor>().ProcessWords(scope.Resolve<ITextParser>().ParseText(reader.ReadFile(config.TextFilePath!)));
            statistic.AddWords(text);
            foreach (var info in scope.Resolve<IWordStatisticsToSizeConverter>().Convert(statistic, config.WordCountToStatistic))
                yield return new Text(info.Word, config.Font, layouter.PutNextRectangle(info.GetCollisionSize()));
        }
    }
}