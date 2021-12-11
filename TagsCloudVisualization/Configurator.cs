using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Features.AttributeFilters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.WordProcessors;
using TagsCloudVisualization.WordValidators;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization
{
    public enum TagCloudResultActions
    {
        Open,
        Save,
        SaveAndOpen,
    }

    public enum SourceTextInterpretationMode
    {
        LiteraryText,
        OneWordPerLine,
    }
    
    public struct Config
    {
        public int WordCountToStatistic { get; set; }
        public Point Center { get; set; }
        public double Density { get; set; }
        public byte MinWordToStatisticLength { get; set; }
        public float MaximumWordFontSize { get; set; }
        
        public string TextFilePath { get; set; }
        public string? CustomIgnoreFilePath { get; set; }
        public string DefaultIgnoreFilePath { get; set; }
        
        public TagCloudResultActions TagCloudResultActions { get; set; }
        public SourceTextInterpretationMode  SourceTextInterpretationMode { get; set; }
    }
    

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
            builder.Register(_ => new DefaultWordStatisticsToSizeConverter(config.MaximumWordFontSize)).As<IWordStatisticsToSizeConverter>();

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
    }
}