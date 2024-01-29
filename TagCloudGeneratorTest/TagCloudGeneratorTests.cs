using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.PointDistributors;

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
            var text = textProcessor.ProcessText(new[] { "Cloud", "Tags" });

            var result = "";
            foreach (var word in text)
                result += (word + Environment.NewLine);

            result.Should().Be("cloud\r\ntags\r\n");
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
            new TestCaseData((object)new []{"but", "tag"}).Returns("tag\r\n").SetName("WithConjunction"),
            new TestCaseData((object)new []{"i", "tag"}).Returns("tag\r\n").SetName("WithPronoun"),
            new TestCaseData((object)new []{"a", "tag"}).Returns("tag\r\n").SetName("WithDeterminer"),
            new TestCaseData((object)new []{"under", "tag"}).Returns("tag\r\n").SetName("WithPreposition"),
        };
    
        [TestOf(nameof(BoringWordsTextProcessor))]
        [TestCaseSource(nameof(BoringWordsTestCases))]
        public string WhenPassTextWithBoringWords_ShouldReturnWordsWithoutBoringWords(IEnumerable<string> values)
        {
            var text = boringWordsTextProcessor.ProcessText(values);

            var result = "";
            foreach (var word in text)
                result += word + Environment.NewLine;

            return result;
        }

        [Test]
        [TestOf(nameof(WordCounter))]
        public void WhenPassTextWithTheMostFrequentlyRepeatedWord_ThisWordShouldBeFirstInDictionary()
        {
            var text = textProcessor.ProcessText(new[] { "tag", "cloud", "cloud", "cloud", "tag" });
            var dictionary = counter.CountWords(text);

            var result = "";

            foreach (var (key , _) in dictionary)
                result += key + " ";

            result.Should().Be("cloud tag ");
        }

        private static TestCaseData[] PathArguments =
        {
            new TestCaseData("../../../TestsData/testFor.txt").Returns("текст из txt ").SetName("WithTxtFormat"),
            new TestCaseData("../../../TestsData/testFor.docx").Returns("текст из docx ").SetName("WitDocxFormat"),
            new TestCaseData("../../../TestsData/testFor.pdf").Returns("текст из pdf ").SetName("WitPdfFormat")
        };

        [TestOf(nameof(TextReader))]
        [TestCaseSource(nameof(PathArguments))]
        public string WhenPassFile_ShouldReturnCorrectResult(string filePath)
        {
            var text = textReader.ReadTextFromFile(filePath);
           
            var result = "";
            foreach (var word in text)
                result += word + " ";

            return result;
        }

        [Test]
        [TestOf(nameof(TagCloudDrawer))]
        public void ShouldReturnCorrectImage()
        {
            var currentBitmap = GetCurrentImage();

            var pathToImage = "C:\\Users\\lholy\\Documents\\GitHub\\di\\TagCloudGeneratorTest\\TestsData\\ForTests.png";
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
            tagCloudDrawer = new TagCloudDrawer(counter, textProcessor, textReader);
            var filePath = "C:\\Users\\lholy\\Documents\\GitHub\\di\\TagCloudGeneratorTest\\TestsData\\test7.txt";
            var settings = new TagsCloudVisualization.VisualizingSettings();
            settings.ImageSize = new Size(1300, 1300);
            settings.PointDistributor = new Spiral(new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2), 1, 0.1);
            settings.ImageName = "currentBitmap.png";

            return tagCloudDrawer.DrawWordsCloud(filePath, settings);
        }
    }
}