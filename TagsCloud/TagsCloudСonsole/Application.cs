using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Razor.Tokenizer;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudTextProcessing.Tokenizers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;
using ITokenizer = TagsCloudTextProcessing.Tokenizers.ITokenizer;

namespace TagsCloudConsole
{
    public class Application
    {
        //protected readonly IEmployeeService _employeeService;
        //protected readonly IPrintService _printService;
        protected readonly string wordsToExcludePath;
        protected readonly ITextReader textReader;
        protected readonly ITextSplitter textSplitter;
        protected readonly IWordsFormatter wordsFormatter;
        protected readonly IWordsExcluder wordsExcluder;
        protected readonly ITokenizer tokenizer;
        protected readonly ITokenShuffler tokenShuffler;


        public Application(
            string wordsToExcludePath,
            ITextReader textReader,
            ITextSplitter textSplitter,
            IWordsFormatter wordsFormatter,
            IWordsExcluder wordsExcluder,
            ITokenizer tokenizer,
            ITokenShuffler tokenShuffler
        )
        {
            this.wordsToExcludePath = wordsToExcludePath;
            this.textReader = textReader;
            this.textSplitter = textSplitter;
            this.wordsFormatter = wordsFormatter;
            this.wordsExcluder = wordsExcluder;
            this.tokenizer = tokenizer;
            this.tokenShuffler = tokenShuffler;
        }

        public void Run()
        {
            var wordsToExclude = File.ReadAllLines(wordsToExcludePath);
            var textInput = textReader.ReadText();
            var wordsInput = textSplitter.SplitText(textInput);
            var wordsAfterFormat = wordsFormatter.Format(wordsInput);
            var wordsAfterExclusion = wordsExcluder.ExcludeWords(wordsAfterFormat, wordsToExclude);
            var tokens = tokenizer.Tokenize(wordsAfterExclusion);
            var shuffledTokens = tokenShuffler.Shuffle(tokens);

            foreach (var t in shuffledTokens)
            {
                Console.WriteLine(t.Word +"  "+t.Count);
            }
            
            //work in progress
            var fontProperties = new FontProperties("Bauhaus 93", 30);
            var style = new Style(new GrayDarkTheme(), fontProperties, new TagSizeCalculatorLogarithmic());
            var visualizer = new TextNoRectanglesVisualizer();
            var layouter = new SpiralCloudLayouter(new Spiral(new PointF(1500, 1500), 0.1f, 0.2f));

           
        }
    }
}