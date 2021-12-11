using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.WordProcessors;

namespace TagCloudUsageSample
{
    public class ClTextOptions : BaseOptions
    {
        private static IContainer container;
        
        [FileValidatorAttribute("invalid file name")]
        [Option('t', "text", Required = true, HelpText = "Text file path")]
        public string TextFilePath { get; private set; }
        
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
        
        [RangeValidatorAttribute(-1, 50, nameof(WordCountToStatistic))]
        [Option('w', "wordCount", Default = -1, HelpText = "Set number of word to statistic.")]
        public int WordCountToStatistic { get; private set; }
        
        public void CreateTags(out string firstFileName)
        {
            container = Configurator.InjectWith(GetConfig());
            firstFileName = Path.Combine(SavePath, FileName) + "." + ImageFormat.Png.ToString().ToLower();
            Console.WriteLine(firstFileName);
            Painter.GetBitmapWithText(GetRectangles()).Save(firstFileName, ImageFormat.Png);
        }

        private IEnumerable<(string, float, Rectangle)> GetRectangles()
        {
            using var scope = container.BeginLifetimeScope();
            var layouter = scope.Resolve<ILayouter<Rectangle>>();
            var statistic = scope.Resolve<IWordsStatistics>();
            var format = TextFormat.GetFormatByExtension(Path.GetExtension(TextFilePath));
            var reader = scope.Resolve<IEnumerable<IFileReader>>().FirstOrDefault(x => x.Format == format) ?? throw new ArgumentException("no proper reader exist");
            var text = scope.Resolve<ITextProcessor>().ProcessWords(scope.Resolve<ITextParser>().ParseText(reader.ReadFile(TextFilePath!)));
            statistic.AddWords(text);
            foreach (var info in scope.Resolve<IWordStatisticsToSizeConverter>().Convert(statistic, WordCountToStatistic))
                yield return (info.Word, info.FontSize, layouter.PutNextRectangle(info.GetCollisionSize()));
        }

        public Config GetConfig()
        {
            return new Config
            {
                WordCountToStatistic = WordCountToStatistic,
                Density = Density,
                MinWordToStatisticLength = (byte)MinWordLengthToStatistic,
                MaximumWordFontSize = MaximumWordFontSize,
                TextFilePath = TextFilePath,
                CustomIgnoreFilePath = IgnoreWordsFileName,
                TagCloudResultActions = Open ? TagCloudResultActions.SaveAndOpen : TagCloudResultActions.Save,
                SourceTextInterpretationMode = IsLiteraryText ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine
            };
        }
    }
}