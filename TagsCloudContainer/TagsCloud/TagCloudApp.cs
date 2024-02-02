using System.Drawing;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;
using System.IO;

namespace TagsCloudContainer.TagsCloud
{
    public class TagCloudApp
    {
        private readonly IPreprocessor _preprocessor;
        private readonly IImageSettings _imageSettings;
        private readonly FileReader _fileReader;
        private readonly FontSizeCalculator _fontSizeCalculator = new FontSizeCalculator();

        private string _fontName;
        private string outputDirectory = @"..\..\..\output";
        private const double DefaultAngleStep = 0.02;
        private const double DefaultRadiusStep = 0.04;
        private const int Half = 2;

        public TagCloudApp(IPreprocessor preprocessor, IImageSettings imageSettings, FileReader fileReader)
        {
            _preprocessor = preprocessor;
            _imageSettings = imageSettings;
            _fileReader = fileReader;
        }


        public void SetFontName(string fontName)
        {
            _fontName = fontName;
        }

        public void Run(CommandLineOptions options)
        {
            SetFontAndImageSettings(options.FontName, options.ImageWidth, options.ImageHeight);

            var wordsResult = _fileReader.ReadFile(options.TextFilePath);

            if (wordsResult.IsSuccess)
            {
                var processedWords = _preprocessor.Process(wordsResult.Value, options.BoringWordsFilePath);
                var uniqueWordCount = CountUniqueWords(processedWords);

                var (fontColor, highlightColor) = GetColors(options.FontColor, options.HighlightColor);

                var tagCloudImage = GenerateTagCloud(processedWords, options.FontName, fontColor, highlightColor, options.PercentageToHighLight);

                SaveTagCloudImage(tagCloudImage, outputDirectory, uniqueWordCount);
            }
            else
            {
                Console.WriteLine($"Error reading file: {wordsResult.ErrorMessage}");
            }
        }

        private (Color fontColor, Color highlightColor) GetColors(string fontColorName, string highlightColorName)
        {
            return (Color.FromName(fontColorName), Color.FromName(highlightColorName));
        }

        private void SetFontAndImageSettings(string fontName, int imageWidth, int imageHeight)
        {
            SetFontName(fontName);
            _imageSettings.UpdateImageSettings(imageWidth, imageHeight);
        }

        public void SaveTagCloudImage(Bitmap tagCloudImage, string outputDirectory, int uniqueWordCount)
        {
            var outputFileName = $"{uniqueWordCount}-tagsCloud.png";
            var outputPath = Path.Combine(outputDirectory, outputFileName);
            tagCloudImage.Save(outputPath);

            Console.WriteLine($"Tag cloud image saved to {outputPath}. Original word count: {uniqueWordCount}");
        }

        private int CountUniqueWords(IEnumerable<string> words)
        {
            var uniqueWords = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
            return uniqueWords.Count;
        }

        private Bitmap GenerateTagCloud(IEnumerable<string> words, string fontName, Color fontColor, Color highlightColor, double percentageToHighlight)
        {
            var layouter = CreateLayouter();
            var uniqueWords = new HashSet<string>();
            var wordFrequencies = _fontSizeCalculator.CalculateWordFrequencies(words);

            var mostPopularWord = GetMostPopularWord(wordFrequencies);

            var sortedWords = SortWordsByFrequency(words, wordFrequencies);

            var fontSizes = CalculateAndPutRectangles(layouter, sortedWords, uniqueWords, wordFrequencies, mostPopularWord, fontName);

            var rectangles = layouter.Rectangles.ToList();

            return Visualizer.VisualizeRectangles(rectangles, uniqueWords, _imageSettings.Width, _imageSettings.Height,
                fontSizes, _fontName, fontColor, highlightColor, percentageToHighlight, wordFrequencies: wordFrequencies);
        }

        private string GetMostPopularWord(Dictionary<string, int> wordFrequencies)
        {
            return wordFrequencies.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;
        }

        private IEnumerable<string> SortWordsByFrequency(IEnumerable<string> words, Dictionary<string, int> wordFrequencies)
        {
            return words.OrderByDescending(word => wordFrequencies[word]);
        }

        private List<int> CalculateAndPutRectangles(CircularCloudLayouter layouter, IEnumerable<string> sortedWords, HashSet<string> uniqueWords, Dictionary<string, int> wordFrequencies, string mostPopularWord, string fontName)
        {
            var fontSizes = new List<int>();

            foreach (var word in sortedWords)
            {
                if (uniqueWords.Add(word))
                {
                    var fontSize = _fontSizeCalculator.CalculateWordFontSize(word, wordFrequencies);
                    fontSizes.Add(fontSize);

                    var font = new Font(fontName, fontSize);
                    layouter.PutNextRectangle(word, font);
                }
            }

            return fontSizes;
        }

        public static Point CalculateCenter(int width, int height)
        {
            return new Point(width / Half, height / Half);
        }

        private CircularCloudLayouter CreateLayouter(double angleStep = DefaultAngleStep, double radiusStep = DefaultRadiusStep)
        {
            var center = CalculateCenter(_imageSettings.Width, _imageSettings.Height);
            var spiral = new Spiral(center, angleStep, radiusStep);
            return new CircularCloudLayouter(center, spiral);
        }
    }
}
