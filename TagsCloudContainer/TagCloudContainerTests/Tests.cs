using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autofac;
using DeepMorphy;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Visualisator;

namespace TagCloudContainerTests
{
    [TestFixture]
    public class Tests
    {
        private string PathToProj = Environment.CurrentDirectory;
        private ImageSettings imageSettings;
        private AlgorithmSettings algoSettings;
        private FileSettings fileSettings;
        private ICloudLayouter cloudLayouter;
        private IParser parser;
        private PictureBox pictureBox;
        private IPainter painter;
        private IWordsCounter wordsCounter;


        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            TagsCloudContainer.Program.RegisterDependencies(builder);
            var container = builder.Build();
            imageSettings = container.Resolve<ImageSettings>();
            algoSettings = container.Resolve<AlgorithmSettings>();
            fileSettings = container.Resolve<FileSettings>();
            fileSettings.SourceFilePath = PathToProj + @"\source.txt";
            fileSettings.CustomBoringWordsFilePath = PathToProj + @"\boring.txt";
            fileSettings.ResultImagePath = PathToProj + @"\image.png";
            parser = container.Resolve<IParser>();
            cloudLayouter = container.Resolve<ICloudLayouter>();
            pictureBox = container.Resolve<PictureBox>();
            painter = container.Resolve<IPainter>();
            wordsCounter = container.Resolve<IWordsCounter>();
        }

        [TestCase(new object[] { "Сл ово" }, new object[] { "чело век" })]
        public void ParserThrowExc_ThanWhiteSpacesInWords(object[] sourceFileText, object[] boringWordsFileText)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", boringWordsFileText.Cast<string>());
            var act1 = () => parser.CountWordsInFile(fileSettings.SourceFilePath);
            var act2 = () => parser.FindWordsInFile(fileSettings.CustomBoringWordsFilePath);

            act1.Should().Throw<ArgumentException>();
        }

        [TestCase(new object[] {"a","a"}, 1)]
        [TestCase(new object[] { "a", "b" }, 2)]
        [TestCase(new object[] { "a", "a", "b" }, 2)]
        public void ParserCountAllWordsInFile_WhenCustomBoringIsEmpty(object[] sourceFileText, int expectedCount)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            var res = parser.CountWordsInFile(fileSettings.SourceFilePath);

            res.Count.Should().Be(expectedCount);
        }

        [TestCase(new object[] { "слово", "a" }, 1)]
        [TestCase(new object[] { "слово", "1" }, 1)]
        [TestCase(new object[] { "слово", "один" }, 1)]
        [TestCase(new object[] { "слово", "и" }, 1)]
        [TestCase(new object[] { "слово", "в" }, 1)]
        [TestCase(new object[] { "слово", "не" }, 1)]
        public void WordsCounterNotCount_SimpleBoringWords(object[] sourceFileText, int expectedCount)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", Array.Empty<string>());
            var res = wordsCounter.CountWords(fileSettings.SourceFilePath, 
                fileSettings.CustomBoringWordsFilePath);

            res.Count.Should().Be(expectedCount);
        }

        [Test]
        public void WordsCounterNotCount_CustomBoringWords()
        {
            FillSourceFile("source.txt", new[] { "слово", "человек" });
            FillSourceFile("boring.txt", new[] { "слово" });
            var res = wordsCounter.CountWords(fileSettings.SourceFilePath,
                fileSettings.CustomBoringWordsFilePath);

            res.Count.Should().Be(1);
        }

        [Test]
        public void ParserTrimmingAndLoweringWords()
        {
            FillSourceFile("source.txt", new[] { " слово ", "чEловек" });
            FillSourceFile("boring.txt", new[] { "\tСкуЧный " });
            var res1 = parser.CountWordsInFile(fileSettings.SourceFilePath);
            var res2 = parser.FindWordsInFile(fileSettings.CustomBoringWordsFilePath);

            res1.ContainsKey("слово").Should().BeTrue();
            res1.ContainsKey("чeловек").Should().BeTrue();
            res2.Contains("скучный").Should().BeTrue();
        }

        [TestCase(new object[0], new object[0], 0)]
        [TestCase(new object[] { "Слово", "человек" }, new object[0], 2)]
        [TestCase(new object[] { "Слово", "слово" }, new object[0], 1)]
        [TestCase(new object[] { "Слово", "человек" }, new object[] { "человек" }, 1)]
        public void LayouterFindRightCountOfRectangles(object[] sourceFileText, object[] boringWordsFileText, int expected)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", boringWordsFileText.Cast<string>());
            var res = GetRectangles();

            res.Count.Should().Be(expected);
        }
        
        [TestCase(new object[] {"Слово", "человек"}, new object[] { 1.0, 1.0 })]
        [TestCase(new object[] {"Слово", "слово", "человек"}, new object[] { 2, 1 })]
        [TestCase(new object[] {"Слово", "Слово", "Слово", "человек", "человек", "человек", "дом", "дом"}
            , new object[] { 3, 3, 2})]
        public void LayouterRightCalculateRectangleSize(object[] sourceFileText, object[] expectedRatio)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", Array.Empty<string>());
            var res = GetRectangles();

            var squares = res
                .Select(e => Convert.ToDouble(e.rectangle.Width * e.rectangle.Height))
                .ToArray();
            var squaresSum = squares.Sum();
            var expected = expectedRatio
                .Select(e => Math.Pow(Convert.ToDouble(e), 2))
                .ToArray();
            var expectedSum = expected.Sum();
            for (int i = 0; i < squares.Length; i++)
            {
                var ratio = squares[i] / squaresSum;
                var exp = expected[i] / expectedSum;
                (Math.Abs(ratio - exp)).Should().BeLessThan(0.1);
            }
        }

        [Test]
        public void PainterSaveImage_CreateFile()
        {
            if (File.Exists(fileSettings.ResultImagePath))
                File.Delete(fileSettings.ResultImagePath);

            pictureBox.RecreateImage(imageSettings);
            painter.Paint(GetRectangles());
            pictureBox.SaveImage(fileSettings.ResultImagePath);

            File.Exists(fileSettings.ResultImagePath).Should().BeTrue();
        }

        private List<(Rectangle rectangle, string text)> GetRectangles()
        {
            var words = wordsCounter.CountWords(fileSettings.SourceFilePath,
                fileSettings.CustomBoringWordsFilePath);
            return cloudLayouter.FindRectanglesPositions(imageSettings.Width, imageSettings.Height, words);
        }

        private void FillSourceFile(string filename, IEnumerable<string> text)
        {
            var path = PathToProj + $@"\{filename}";
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(string.Join("\n", text));
                fstream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
