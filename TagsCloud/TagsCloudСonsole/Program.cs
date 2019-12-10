using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudTextProcessing.Tokenizers;
using TagsCloudVisualization;
using TagsCloudVisualization.BitmapSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var wordsToExclude = File.ReadAllLines(Path.Combine(projectDirectory, "Examples","Docx", "exclude.txt"));
            var textInput = new DocxTextReader().ReadText(Path.Combine(projectDirectory, "Examples","Docx", "pixel art.docx"));

            var wordsInput = new TextSplitter(@"[^a-zA-Z]+").SplitText(textInput);
            var wordsAfterFormat = new WordsFormatterLowercaseAndTrim().Format(wordsInput);
            var wordsAfterExclusion = new WordsExcluder().ExcludeWords(wordsAfterFormat, wordsToExclude);
            var tokens = new Tokenizer().Tokenize(wordsAfterExclusion);
            var shuffledTokens = new TokenShufflerAscending().Shuffle(tokens);
            
            //Bauhaus 93
            var fontProperties = new FontProperties("Bauhaus 93", 30);
            var style = new Style(new GrayDarkTheme(), fontProperties, new TagSizeCalculatorLogarithmic());
            var visualizer = new TextNoRectanglesVisualizer();
            var layouter = new SpiralCloudLayouter(new Spiral(new PointF(1500, 1500), 0.1f, 0.2f));
            
            var cloud = new Cloud(shuffledTokens, style, visualizer, layouter);

            var bitmap = cloud
                .Visualize(3000, 3000);
            
   
            new PngBitmapSaver().Save(bitmap, Path.Combine(projectDirectory, "Examples","Docx","result"));
            bitmap.Dispose();
            
        }
    }
}