using System.IO;
using System.Linq;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudVisualization.BitmapSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.TagColorizer;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;
using ITokenizer = TagsCloudTextProcessing.Tokenizers.ITokenizer;

namespace TagsCloudConsole
{
    public class Application
    {
        protected readonly string wordsToExcludePath;
        protected readonly ITextReader textReader;
        protected readonly ITextSplitter textSplitter;
        protected readonly IWordsFormatter wordsFormatter;
        protected readonly IWordsExcluder wordsExcluder;
        protected readonly ITokenizer tokenizer;
        protected readonly ITokenShuffler tokenShuffler;
        protected readonly FontProperties fontProperties;
        protected readonly ITheme theme;
        protected readonly TagSizeCalculator tagSizeCalculator;
        protected readonly ITagColorizer tagColorizer;
        protected readonly  ICloudVisualizer cloudVisualizer;
        protected readonly ICloudLayouter cloudLayouter;
        protected readonly  IBitmapSaver bitmapSaver;
        protected readonly string imageOutputPath;
        protected readonly int width;
        protected readonly int height;

        public Application(
            string wordsToExcludePath,
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
            IBitmapSaver bitmapSaver,
            int width,
            int height,
            string imageOutputPath
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
            /*foreach (var t in enumerable)
                Console.WriteLine(t.Word +"  "+t.Count);*/
            var style = new Style(theme, fontProperties, tagSizeCalculator, tagColorizer);
            var tags = cloudLayouter.GenerateTagsSequence(style, enumerable);
            using (var bitmap = cloudVisualizer.Visualize(style, tags, width, height))
                bitmapSaver.Save(bitmap, imageOutputPath);
        }
    }
}