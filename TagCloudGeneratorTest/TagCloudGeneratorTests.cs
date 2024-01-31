using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.PointDistributors;
using System.Linq;

namespace TagCloudGeneratorTest
{
    public class Tests
    {
        private TextProcessor textProcessor;
        private WordCounter counter;
        private BoringWordsTextProcessor boringWordsTextProcessor;
        private TextReader textReader;
        private TagCloudDrawer tagCloudDrawer;

        [SetUp]
        public void Setup()
        {
            textProcessor = new TextProcessor();
            counter = new WordCounter();
            boringWordsTextProcessor = new BoringWordsTextProcessor(textProcessor);
            textReader = new TextReader();            
        }

        [Test]
        [TestOf(nameof(TextProcessor))]
        public void WhenPassWordsInUppercase_ShouldReturnWordsInLowerCase()
        {
            var text = textProcessor.ProcessText(new[] { "Cloud"}).ToArray();

            text[0].Should().Be("cloud");
        }

        [Test]
        [TestOf(nameof(WordCounter))]
        public void WhenWordIsRepeatedSeveralTimesInText_ItShouldBeOutputOneTime()
        {
            var text = textProcessor.ProcessText(new[] { "cloud", "tag", "cloud" });
            var dictionary = counter.CountWords(text);

            dictionary["cloud"].Should().Be(2);
        }

        private static TestCaseData[] BoringWordsTestCases =
        {
            new TestCaseData((object)new []{"but", "tag"}).Returns("tag").SetName("WithConjunction"),
            new TestCaseData((object)new []{"i", "tag"}).Returns("tag").SetName("WithPronoun"),
            new TestCaseData((object)new []{"a", "tag"}).Returns("tag").SetName("WithDeterminer"),
            new TestCaseData((object)new []{"under", "tag"}).Returns("tag").SetName("WithPreposition"),
        };
    
        [TestOf(nameof(BoringWordsTextProcessor))]
        [TestCaseSource(nameof(BoringWordsTestCases))]
        public string WhenPassTextWithBoringWords_ShouldReturnWordsWithoutBoringWords(IEnumerable<string> values)
        {
            var text = boringWordsTextProcessor.ProcessText(values).ToArray();

            return text[0];
        }

        [Test]
        [TestOf(nameof(WordCounter))]
        public void WhenPassTextWithTheMostFrequentlyRepeatedWord_ThisWordShouldBeFirstInDictionary()
        {
            var text = textProcessor.ProcessText(new[] { "tag", "cloud", "cloud", "cloud", "tag" }).ToArray();
            var dictionary = counter.CountWords(text);

            var result = new List<string>(2);
            foreach (var (key, _) in dictionary)
                result.Add(key);

            result[0].Should().Be("cloud");
        }

        private static TestCaseData[] PathArguments =
        {
            new TestCaseData("../../../TestsData/testFor.txt").Returns(new [] {"текст", "из", "txt"}).SetName("WithTxtFormat"),
            new TestCaseData("../../../TestsData/testFor.docx").Returns(new [] {"текст", "из", "docx"}).SetName("WitDocxFormat"),
            new TestCaseData("../../../TestsData/testFor.pdf").Returns(new [] {"текст", "из", "pdf"}).SetName("WitPdfFormat")
        };

        [TestOf(nameof(TextReader))]
        [TestCaseSource(nameof(PathArguments))]
        public string[] WhenPassFile_ShouldReturnCorrectResult(string filePath) => textReader.ReadTextFromFile(filePath).ToArray();

        [Test]
        [TestOf(nameof(TagCloudDrawer))]
        public void ShouldReturnCorrectImage()
        {
            var currentBitmap = GetCurrentImage();           

            var pathToImage = "../../../TestsData/ForTests.png";
            Bitmap correctBitmap = new Bitmap(pathToImage);

            var result = true;
            if (currentBitmap != null && correctBitmap != null)
            {
                if (currentBitmap.Width != correctBitmap.Height || currentBitmap.Height != correctBitmap.Height)
                    result = false;

                for (int column = 0; column < correctBitmap.Width; column++)
                    for (int row = 0; row < correctBitmap.Height; row++)
                    {
                        if (!correctBitmap.GetPixel(column, row).Equals(currentBitmap.GetPixel(column, row)))
                            result = false;
                    }
            }
            result.Should().BeTrue();   
        }

        private Bitmap GetCurrentImage()
        {
            tagCloudDrawer = new TagCloudDrawer(counter, boringWordsTextProcessor, textReader);
            var filePath = "../../../TestsData/test7.txt";
            var settings = new TagsCloudVisualization.VisualizingSettings();
            settings.ImageSize = new Size(1300, 1300);
            settings.PointDistributor = new Spiral(new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2), 1, 0.1);
            settings.ImageName = "currentBitmap.png";

            return tagCloudDrawer.DrawWordsCloud(filePath, settings);
        }
    }
}