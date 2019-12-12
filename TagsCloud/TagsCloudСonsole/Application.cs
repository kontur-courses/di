using System.Linq;
using TagsCloudTextProcessing.Filters;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Tokenizers;
using TagsCloudTextProcessing.WordsIntoTokensTranslators;
using TagsCloudVisualization.BitmapSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.TagColorizer;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudConsole
{
    public class Application
    {
        private readonly IBitmapSaver bitmapSaver;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ICloudVisualizer cloudVisualizer;
        private readonly FontProperties fontProperties;
        private readonly int height;
        private readonly string imageOutputPath;
        private readonly ITagColorizer tagColorizer;
        private readonly TagSizeCalculator tagSizeCalculator;
        private readonly ITextReader textReader;
        private readonly ITokenizer tokenizer;
        private readonly ITheme theme;
        private readonly IWordsIntoTokenTranslator wordsIntoTokenTranslator;
        private readonly ITokenShuffler tokenShuffler;
        private readonly int width;
        private readonly IWordsFilter wordsFilter;
        private readonly IWordsFormatter wordsFormatter;

        public Application(
            int width,
            int height,
            string imageOutputPath,
            ITextReader textReader,
            ITokenizer tokenizer,
            IWordsFormatter wordsFormatter,
            IWordsFilter wordsFilter,
            IWordsIntoTokenTranslator wordsIntoTokenTranslator,
            ITokenShuffler tokenShuffler,
            FontProperties fontProperties,
            ITheme theme,
            TagSizeCalculator tagSizeCalculator,
            ITagColorizer tagColorizer,
            ICloudVisualizer cloudVisualizer,
            ICloudLayouter cloudLayouter,
            IBitmapSaver bitmapSaver
        )
        {
            this.textReader = textReader;
            this.tokenizer = tokenizer;
            this.wordsFormatter = wordsFormatter;
            this.wordsFilter = wordsFilter;
            this.wordsIntoTokenTranslator = wordsIntoTokenTranslator;
            this.tokenShuffler = tokenShuffler;
            this.fontProperties = fontProperties;
            this.theme = theme;
            this.tagSizeCalculator = tagSizeCalculator;
            this.tagColorizer = tagColorizer;
            this.cloudVisualizer = cloudVisualizer;
            this.cloudLayouter = cloudLayouter;
            this.bitmapSaver = bitmapSaver;
            this.width = width;
            this.height = height;
            this.imageOutputPath = imageOutputPath;
        }

        public void Run()
        {
            var textInput = textReader.ReadText();
            var wordsInput = tokenizer.Tokenize(textInput);
            var words = wordsFormatter.Format(wordsInput);
            words = wordsFilter.Filter(words);
            var tokens = wordsIntoTokenTranslator.TranslateIntoTokens(words);
            var shuffledTokens = tokenShuffler.Shuffle(tokens);
            var enumerable = shuffledTokens.ToList();
            var style = new Style(theme, fontProperties, tagSizeCalculator, tagColorizer);
            var tags = cloudLayouter.GenerateTagsSequence(style, enumerable);
            using (var bitmap = cloudVisualizer.Visualize(style, tags, width, height))
            {
                bitmapSaver.Save(bitmap, imageOutputPath);
            }
        }
    }
}