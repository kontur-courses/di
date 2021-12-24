using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ResultProject;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.Printing;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Statistics;
using TagsCloudVisualization.WordValidators;

[assembly: InternalsVisibleTo("TagsCloudVisualizationTest")]
namespace TagsCloudVisualization
{
    public class TagCloud
    {
        private readonly IEnumerable<IFileReader> fileReaders;
        
        private readonly LiteraryTextParser literaryTextParser;
        private readonly LinesParser linesParser;
        
        private readonly LiteraryWordsStatistics literaryWordsStatistics;
        private readonly OneWordByLineStatistics oneWordByLineStatistics;
        
        private readonly IgnoredWordsValidator ignoredWordsValidator;
        private readonly TooShortWordValidator tooShortWordValidator;
        
        private readonly IWordStatisticsToSizeConverter wordStatisticsToSizeConverter;
        
        private readonly RandomColorScheme randomColorScheme;
        private readonly SingleColorScheme singleColorScheme;
        
        private readonly IPrinter<Text> printer;
        
        public InfoTagsCloud Info { get; }

        
        public TagCloud(
            IEnumerable<IFileReader> fileReaders, 
            LiteraryTextParser literaryTextParser, 
            LinesParser linesParser, 
            LiteraryWordsStatistics literaryWordsStatistics, 
            OneWordByLineStatistics oneWordByLineStatistics, 
            IgnoredWordsValidator ignoredWordsValidator, 
            TooShortWordValidator tooShortWordValidator, 
            IWordStatisticsToSizeConverter wordStatisticsToSizeConverter, 
            RandomColorScheme randomColorScheme, 
            SingleColorScheme singleColorScheme, 
            IPrinter<Text> printer, 
            InfoTagsCloud info)
        {
            this.fileReaders = fileReaders;
            this.literaryTextParser = literaryTextParser;
            this.linesParser = linesParser;
            this.literaryWordsStatistics = literaryWordsStatistics;
            this.oneWordByLineStatistics = oneWordByLineStatistics;
            this.ignoredWordsValidator = ignoredWordsValidator;
            this.tooShortWordValidator = tooShortWordValidator;
            this.wordStatisticsToSizeConverter = wordStatisticsToSizeConverter;
            this.randomColorScheme = randomColorScheme;
            this.singleColorScheme = singleColorScheme;
            this.printer = printer;
            Info = info;
        }
        
        private Result<string> ReadFile(string filePath)
        {
            return Path.GetExtension(filePath).AsResult()
                .Then(TextFormat.GetFormatByExtension)
                .Then(x => fileReaders.FirstOrDefault(y => y.Format == x))
                .ValidateNull("no proper reader exist")
                .Then(x => x!.ReadFile(filePath));
        }

        private IEnumerable<string> ParseText(SourceTextInterpretationMode sourceTextInterpretationMode, string text)
        {
            return sourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                ? literaryTextParser.ParseText(text)
                : linesParser.ParseText(text);
        }

        private IWordsStatistics GetStatisticService(SourceTextInterpretationMode sourceTextInterpretationMode)
        {
            return sourceTextInterpretationMode == SourceTextInterpretationMode.LiteraryText
                ? literaryWordsStatistics.CreateStatistics()
                : oneWordByLineStatistics.CreateStatistics();
        }

        private void SetIgnoreWords(string? ignoreFilePath)
        {
            if (ignoreFilePath is not null)
                ReadFile(ignoreFilePath).AsResult()
                    .Then(y => linesParser.ParseText(y.GetValueOrThrow()!))
                    .ThenAction(y => ignoredWordsValidator.SetIgnoreWords(y));
        }

        private Result<IEnumerable<TagWordInfo>> CalculateStatistic(Config config, IEnumerable<string> parsedText)
        {
            return GetStatisticService(config.SourceTextInterpretationMode).AsResult()
                .ThenAction(x => x.AddWords(parsedText))
                .Then(x => wordStatisticsToSizeConverter.Convert(x, config.WordCountToStatistic));
        }

        private static ILayouter<Rectangle> GetLayouter(Config config)
        {
            return new CircularCloudLayouter(new PointSpiral(Point.Empty, config.Density, config.Density));
        }

        private static Result<IEnumerable<Text>> GetTextToPrint(ILayouter<Rectangle> layouter, IEnumerable<TagWordInfo> words, string font)
        {
            return words.AsResult()
                .ThenForEach(x => new Text(x.Word, font, layouter.PutNextRectangle(x.GetCollisionSize()).GetValueOrThrow()).AsResult());
        }

        private IColorScheme SetColor(Color? color)
        {
            IColorScheme colorScheme = color is null
                ? randomColorScheme
                : singleColorScheme;
            if (color is not null) 
                ((SingleColorScheme)colorScheme).SetColor(color.Value);

            return colorScheme;
        }

        public Result<Bitmap> GetBitmap(Config config)
        {
            return ReadFile(config.TextFilePath)
                .ThenAction(_ => tooShortWordValidator.SetLimit(config.MinWordToStatisticLength))
                .ThenAction(_ => SetIgnoreWords(config.IgnoreFilePath))
                .Then(text => ParseText(config.SourceTextInterpretationMode, text))
                .Then(parsedText => CalculateStatistic(config, parsedText))
                .Then(calculatedWords => (calculatedWords, layouter: GetLayouter(config)))
                .Then(x => GetTextToPrint(x.layouter, x.calculatedWords, config.Font))
                .Then(x => printer.GetBitmap(SetColor(config.Color), x, config.Size));
        }
    }
}