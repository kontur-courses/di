using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeepMorphy;
using FluentAssertions;
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
        private static string asd = Environment.CurrentDirectory;
        private string PathToProj = Environment.CurrentDirectory;
        private ImageSettings imageSettings;
        private AlgorithmSettings algoSettings;
        private FileSettings fileSettings;
        private ICloudLayouter cloudLayouter;
        private IParser parser;
        private PictureBox pictureBox;
        private IPainter painter;


        [SetUp]
        public void SetUp()
        {
            imageSettings = new ImageSettings();
            algoSettings = new AlgorithmSettings();
            fileSettings = new FileSettings();
            fileSettings.SourceFilePath = PathToProj + @"\source.txt";
            fileSettings.CustomBoringWordsFilePath = PathToProj + @"\boring.txt";
            fileSettings.ResultImagePath = PathToProj + @"\image.png";
            parser = new Parser(fileSettings, new MorphAnalyzer());
            cloudLayouter = new CircularCloudLayouter(imageSettings, algoSettings, parser);
            pictureBox = new PictureBox();
            painter = new TagCloudPainter(pictureBox, imageSettings);
        }

        [TestCase("asd", null)]
        [TestCase(null, "asd123")]
        public void FileSettingThrowException_ThanIncorrectArgs(string? sourcePath, string? customBoringPath)
        {
            fileSettings.SourceFilePath = sourcePath != null ? sourcePath 
                : fileSettings.SourceFilePath;
            fileSettings.CustomBoringWordsFilePath = customBoringPath != null ? customBoringPath 
                : fileSettings.CustomBoringWordsFilePath;

            var act = () => fileSettings.ThrowExcIfFileNotFound();

            act.Should().Throw<FileNotFoundException>();
        }

        [TestCase(0,1)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void AlgoSettingThrowException_ThanIncorrectArgs(double dr, double fi)
        {
            algoSettings.Dr = dr;
            algoSettings.Fi = fi;

            var act = () => algoSettings.ThrowExcIfNonPositiveArgs();

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void ImageSettingThrowException_ThanIncorrectArgs(int width, int height)
        {
            imageSettings.Width = width;
            imageSettings.Height = height;

            var act = () => imageSettings.ThrowExcIfNonPositiveArgs();

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(new object[] { "Сл ово" }, new object[0])]
        public void ParserThrowExc_ThanWhiteSpacesInWords(object[] sourceFileText, object[] boringWordsFileText)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", boringWordsFileText.Cast<string>());
            var act = () => parser.GetWordsCountWithoutBoring();

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(new object[] {"a","a"}, 1)]
        [TestCase(new object[] { "a", "b" }, 2)]
        [TestCase(new object[] { "a", "a", "b" }, 2)]
        public void ParserCountAllWordsInFile_WhenCustomBoringIsEmpty(object[] sourceFileText, int expectedCount)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            var res = parser.CountWordsInSourceFile();

            res.Count.Should().Be(expectedCount);
        }

        [TestCase(new object[] { "слово", "a" }, 1)]
        [TestCase(new object[] { "слово", "1" }, 1)]
        [TestCase(new object[] { "слово", "один" }, 1)]
        [TestCase(new object[] { "слово", "и" }, 1)]
        [TestCase(new object[] { "слово", "в" }, 1)]
        [TestCase(new object[] { "слово", "не" }, 1)]
        public void ParserNotCount_SimpleBoringWords(object[] sourceFileText, int expectedCount)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", Array.Empty<string>());
            var res = parser.GetWordsCountWithoutBoring();

            res.Count.Should().Be(expectedCount);
        }

        [Test]
        public void ParserNotCount_CustomBoringWords()
        {
            FillSourceFile("source.txt", new[] { "слово", "человек" });
            FillSourceFile("boring.txt", new[] { "слово" });
            var res = parser.GetWordsCountWithoutBoring();

            res.Count.Should().Be(1);
        }

        [Test]
        public void ParserTrimmingAndLoweringWords()
        {
            FillSourceFile("source.txt", new[] { " слово ", "чEловек", "скучный" });
            FillSourceFile("boring.txt", new[] { " СкуЧный" });
            var res = parser.GetWordsCountWithoutBoring();

            res.ContainsKey("слово").Should().BeTrue();
            res.ContainsKey("чeловек").Should().BeTrue();
            res.ContainsKey("скучный").Should().BeFalse();
        }

        [TestCase(new object[0], new object[0], 0)]
        [TestCase(new object[] { "Слово", "человек" }, new object[0], 2)]
        [TestCase(new object[] { "Слово", "слово" }, new object[0], 1)]
        [TestCase(new object[] { "Слово", "человек" }, new object[] { "человек" }, 1)]
        public void LayouterFindRightCountOfRectangles(object[] sourceFileText, object[] boringWordsFileText, int expected)
        {
            FillSourceFile("source.txt", sourceFileText.Cast<string>());
            FillSourceFile("boring.txt", boringWordsFileText.Cast<string>());
            var res = cloudLayouter.FindRectanglesPositions();

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
            var res = cloudLayouter.FindRectanglesPositions();

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
            painter.Paint(cloudLayouter.FindRectanglesPositions());
            pictureBox.SaveImage(fileSettings.ResultImagePath);

            File.Exists(fileSettings.ResultImagePath).Should().BeTrue();
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
