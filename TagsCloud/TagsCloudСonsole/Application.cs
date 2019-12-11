using System.IO;
using System.Linq;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudTextProcessing.Tokenizers;
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
        private readonly ITextSplitter textSplitter;
        private readonly ITheme theme;
        private readonly ITokenizer tokenizer;
        private readonly ITokenShuffler tokenShuffler;
        private readonly int width;
        private readonly IWordsExcluder wordsExcluder;
        private readonly IWordsFormatter wordsFormatter;
        private readonly string wordsToExcludePath;

        public Application(
            string wordsToExcludePath,
            int width,
            int height,
            string imageOutputPath,
            ITextReader textReader,
            ITextSplitter textSplitter,
            IWordsFormatter wordsFormatter,
            IWordsExcluder wordsExcluder,
            ITokenizer tokenizer,
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
            this.wordsToExcludePath = wordsToExcludePath;
            this.textReader = textReader;
            this.textSplitter = textSplitter;
            this.wordsFormatter = wordsFormatter;
            this.wordsExcluder = wordsExcluder;
            this.tokenizer = tokenizer;
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
            var wordsInput = textSplitter.SplitText(textInput);
            var words = wordsFormatter.Format(wordsInput);
            if (wordsToExcludePath != "none")
            {
                var wordsToExclude = File.ReadAllLines(wordsToExcludePath);
                words = wordsExcluder.ExcludeWords(words, wordsToExclude);
            }

            var tokens = tokenizer.Tokenize(words);
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