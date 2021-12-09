using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using Autofac.Features.AttributeFilters;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.WordReaders;
using TagsCloudVisualization.WordReaders.FormatDecoders;
using TagsCloudVisualization.WordReaders.WordProcessors;
using TagsCloudVisualization.WordReaders.WordValidators;
using WeCantSpell.Hunspell;

namespace TagCloudUsageSample
{
    public class ClTextOptions : BaseOptions
    {
        private static IContainer container;
        
        [FileValidatorAttribute("invalid file name")]
        [Option('t', "text", Required = true, HelpText = "Text file path")]
        public string TextFileName { get; private set; }
        
        [PathValidatorAttribute("unknown directory")]
        [Option('p', "path", Default = "..\\..\\", HelpText = "Set path to save tag clouds.")]
        public string SavePath{ get; private set; }
        
        [FileNameValidatorAttribute("invalid file name")]
        [Option('n', "name", Default = "TagCloud", HelpText = "Set name to save tag clouds.")]
        public string FileName { get; private set; }
        
        [FileValidatorAttribute("invalid file name")]
        [Option('i', "ignore", HelpText = "Ignore words file path")]
        public string IgnoreWordsFileName { get; private set; }
        
        [Option('o', "open", Default = false, HelpText = "Open created file")]
        public bool Open { get; set; }
        
        [Option('l', "isLiteraryText", Default = false, HelpText = "Is text literary")]
        public bool IsLiteraryText { get; set; }
        
        [RangeValidatorAttribute(1, 50, nameof(Density))]
        [Option('d', "density", Default = 5, HelpText = "Set density")]
        public int Density { get; private set; }
        
        [RangeValidatorAttribute(1, 50, nameof(MinWordLengthToStatistic))]
        [Option('m', "wordLength", Default = 3, HelpText = "Set min word length to statistic")]
        public int MinWordLengthToStatistic { get; private set; }
        
        [RangeValidatorAttribute(25, 400, nameof(MaximumWordFontSize))]
        [Option('f', "font", Default = 60, HelpText = "Set font size for most common word.")]
        public int MaximumWordFontSize{ get; private set; }
        
        public void CreateTags(out string firstFileName)
        {
            Inject();
            firstFileName = Path.Combine(SavePath, FileName) + "." + ImageFormat.Png.ToString().ToLower();
            Console.WriteLine(firstFileName);
            Painter.GetBitmapWithText(GetRectangles()).Save(firstFileName, ImageFormat.Png);
        }

        private static IEnumerable<(string, float, Rectangle)> GetRectangles()
        {
            var layouter = container.BeginLifetimeScope().Resolve<ILayouter<Rectangle>>();
            foreach (var info in container.BeginLifetimeScope().Resolve<IWordStatisticsToSizeConverter>().Convert())
                yield return (info.Word, info.FontSize, layouter.PutNextRectangle(info.GetCollisionSize()));
        }

        private void Inject()
        {
            var builder = new ContainerBuilder();
            
            builder.Register(_ => new PointSpiral(Point.Empty, Density, Density)).As<IInfinityPointsEnumerable>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter<Rectangle>>();
            builder.RegisterType<ProcessedWordReader>().Keyed<IWordReader>("FullProcessed").WithAttributeFiltering();
            builder.RegisterType<TxtFormatDecoder>().As<IFormatDecoder>();
            builder.RegisterType<IgnoredWordsValidator>().As<IWordValidator>().WithAttributeFiltering();
            builder.Register(_ => new TooShortWordValidator(MinWordLengthToStatistic)).As<IWordValidator>();
            builder.RegisterType<LowerCaseWordProcessor>().As<IWordProcessor>();

            if (!IsLiteraryText)
            {
                builder.Register(p => new FileWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("CurrentReadMode");
            }
            else
            {
                builder.Register(p => new FileTextByWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("CurrentReadMode");
                builder.RegisterType<InitialFormWordProcessor>().As<IWordProcessor>();
                builder.Register(_ => WordList.CreateFromFiles(@"Russian.dic")).As<WordList>();
            }
            
            builder.Register(p => new FileWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("Word");
            builder.Register(p => new FileTextByWordReader(TextFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("Text");
            builder.Register(p => new FileWordReader(IgnoreWordsFileName, p.Resolve<IEnumerable<IFormatDecoder>>())).Keyed<IWordReader>("IgnoreWords");
            builder.RegisterType<WordsStatistics>().As<IWordsStatistics>().OnActivated(s => s.Instance.Load()).WithAttributeFiltering();
            builder.Register(p => new DefaultWordStatisticsToSizeConverter(MaximumWordFontSize, p.Resolve<IWordsStatistics>())).As<IWordStatisticsToSizeConverter>();

            container = builder.Build();
        }
    }
}