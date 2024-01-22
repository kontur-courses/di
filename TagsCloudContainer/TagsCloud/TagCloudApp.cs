using System.Drawing;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;
using TagsCloudContainer.Readers;

namespace TagsCloudContainer.TagsCloud
{
    public class TagCloudApp
    {
        private readonly IPreprocessor _preprocessor;
        private readonly IImageSettings _imageSettings;
        private string _fontName;

        public TagCloudApp(IPreprocessor preprocessor, IImageSettings imageSettings)
        {
            _preprocessor = preprocessor;
            _imageSettings = imageSettings;
        }

        public void SetFontName(string fontName)
        {
            _fontName = fontName;
        }

        public void Run(string filePath, string boringWordsFilePath, string fontName, Color fontColor, Color highlightColor, double percentageToHighlight)
        {
            SetFontName(fontName);

            var words = ReadFile(filePath);
            var processedWords = _preprocessor.Process(words, boringWordsFilePath);

            Console.WriteLine("Processed words:\n");

            foreach (var item in processedWords)
            {
                Console.WriteLine(item);
            }

            var tagCloudImage = GenerateTagCloud(processedWords, fontName, fontColor, highlightColor, percentageToHighlight);

            tagCloudImage.Save(@"..\..\..\output\tagsCloud.png");
            Console.WriteLine("Tag cloud image saved to: /output/");
        }

        private IEnumerable<string> ReadFile(string filePath)
        {
            var fileReader = GetFileReader(filePath);
            return fileReader.ReadWords(filePath);
        }

        private IFileReader GetFileReader(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath)?.ToLower();

            switch (fileExtension)
            {
                case ".doc":
                    return new DocReader();
                case ".docx":
                    return new DocxReader();
                case ".txt":
                    return new TxtReader();
                default:
                    throw new InvalidOperationException("Unsupported file extension");
            }
        }

        private Bitmap GenerateTagCloud(IEnumerable<string> words, string fontName, Color fontColor, Color highlightColor, double percentageToHighlight)
        {
            var layouter = CreateLayouter();
            var uniqueWords = new HashSet<string>();
            var wordFrequencies = CalculateWordFrequencies(words);

            var mostPopularWord = wordFrequencies.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;

            var sortedWords = words.OrderByDescending(word => wordFrequencies[word]);

            var fontSizes = new List<int>();

            foreach (var word in sortedWords)
            {
                if (uniqueWords.Add(word))
                {
                    var fontSize = CalculateFontSize(word, wordFrequencies, mostPopularWord);
                    fontSizes.Add(fontSize);

                    var font = new Font(fontName, fontSize);
                    layouter.PutNextRectangle(word, font);
                }
            }

            var rectangles = layouter.Rectangles.ToList();

            return Visualizer.VisualizeRectangles(rectangles, uniqueWords,  _imageSettings.ImageWidth, _imageSettings.ImageHeight, 
                fontSizes, _fontName, fontColor, highlightColor, percentageToHighlight, wordFrequencies: wordFrequencies);
        }

        private Dictionary<string, int> CalculateWordFrequencies(IEnumerable<string> words)
        {
            var wordFrequencies = new Dictionary<string, int>();

            foreach (var word in words)
            {
                wordFrequencies.TryGetValue(word, out var frequency);
                wordFrequencies[word] = frequency + 1;
            }

            return wordFrequencies;
        }

        private CircularCloudLayouter CreateLayouter()
        {
            var center = new Point(_imageSettings.ImageWidth / 2, _imageSettings.ImageHeight / 2);
            var spiral = new Spiral(center, 0.02, 0.04);
            return new CircularCloudLayouter(center, spiral);
        }

        private int CalculateFontSize(string word, Dictionary<string, int> wordFrequencies, string mostPopularWord)
        {
            if (wordFrequencies.TryGetValue(word, out var frequency))
            {
                return Math.Max(30, 30 + frequency * 2);
            }
            return 10;
        }
    }
}
