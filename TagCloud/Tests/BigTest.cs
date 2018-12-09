using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    public class BigTest
    {
        [Test]
        public void DoSomething_WhenSomething()
        {
            var textPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Text.docx");
            var parser = new WordsFromMicrosoftWord(textPath);
            var lowerParser = new LowerWord(parser);
            var boringWordsFromFile =
                new WordsFromFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "BoringWords.txt"));
            var boringParser = new BoringWordsFilter(boringWordsFromFile.GetWords(), lowerParser.ToLower());
            var frequency = new FrequencyCollection();
            var frequencyNormalizedCollection = frequency.GetFrequencyCollection(boringParser.DeleteBoringWords());
            var center = new Point(0, 0);
            var width = 0.1;
            var step = 0.01;
            var layout =
                new TagCloudLayouter(new CircularCloudLayouter(center, new CircularSpiral(center, width, step)));
            var wordWithCoordinate = layout.GetLayout(frequencyNormalizedCollection);
            var color = Color.Black;
            var imageSize = new Size(1000, 1000);
            var coordinatesAtImage = new CoordinatesAtImage(imageSize);
            var coordinates = coordinatesAtImage.GetCoordinates(wordWithCoordinate);
            var fontFamily = new FontFamily("Consolas");
            var imageFormat = ImageFormat.Png;
            var imageName = Path.Combine(TestContext.CurrentContext.TestDirectory, "BigCloud.png");
            var graphics = new Picture(imageSize, fontFamily, color, imageFormat, imageName);
            graphics.Save(coordinates);
        }
    }
}