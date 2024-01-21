using System.Drawing.Imaging;
using System.Drawing;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.TagsCloud
{
    public class TagCloudApp
    {
        private readonly IFileReader _fileReader;
        private readonly IPreprocessor _preprocessor;
        private readonly ITagCloudGenerator _tagCloudGenerator;
        private readonly IImageSettings _imageSettings;

        public TagCloudApp(IFileReader fileReader, IPreprocessor preprocessor, ITagCloudGenerator tagCloudGenerator, IImageSettings imageSettings)
        {
            _fileReader = fileReader;
            _preprocessor = preprocessor;
            _tagCloudGenerator = tagCloudGenerator;
            _imageSettings = imageSettings;
        }

        public void Run(string filePath)
        {
            var words = _fileReader.ReadWords(filePath);
            var processedWords = _preprocessor.Process(words);

            Console.WriteLine("Processed words:\n");

            foreach (var item in processedWords)
            {
                Console.WriteLine(item);
            }

            var tagCloudImage = GenerateTagCloud(processedWords);

            tagCloudImage.Save(@"..\..\..\output\tagsCloud.png", ImageFormat.Png);

            Console.WriteLine("Tag cloud image saved to: /output/");
        }

        private Bitmap GenerateTagCloud(IEnumerable<string> words)
        {
            var layouter = CreateLayouter();
            var uniqueWords = new HashSet<string>();
            var wordFrequencies = CalculateWordFrequencies(words);

            var mostPopularWord = wordFrequencies.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;

            var sortedWords = words.OrderByDescending(word => wordFrequencies[word]);

            foreach (var word in sortedWords)
            {
                if (uniqueWords.Add(word)) // Проверяем и добавляем в хеш-сет, возвращая true, если слово уникально
                {
                    var font = new Font("Arial", CalculateFontSize(word, wordFrequencies, mostPopularWord));
                    layouter.PutNextRectangle(word, font);
                }
            }

            var rectangles = layouter.Rectangles.ToList();
            return Visualizer.VisualizeRectangles(rectangles, uniqueWords, _imageSettings.ImageWidth, _imageSettings.ImageHeight);
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
                return Math.Max(35, 35 + frequency * 2);
            }
            return 10;
        }
    }
}
