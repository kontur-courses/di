using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudTests
    {
        private Random random = new Random();
        private TagCloudFactory factory = new TagCloudFactory();
        private TagCloud tagCloud;
        private string sourceFile = "testIn.txt";
        private const string DefaultContent = "Tag tag  tag cloud cloud test";
        private string resultFile;
        
        [SetUp]
        public void SetUp()
        {
            sourceFile = random.Next() + ".txt";
            resultFile = random.Next() + ".png";
            tagCloud = factory.CreateInstance(false, "sorted");
            CreateFile(sourceFile, DefaultContent);
        }

        private void CreateFile(string path, string content)
        {
            using var file = File.CreateText(path);
            file.Write(content);
        }

        [TearDown]
        public void DeleteFiles()
        {
            try
            {
                File.Delete(sourceFile);
                File.Delete(resultFile);
            }
            catch
            {
                // ignored
            }
        }

        [TestCase(-100, 100)]
        [TestCase(100, 0)]
        [TestCase(-100, -100)]
        public void TagCloud_NonPositiveResolution_Throws(int width, int height)
        {
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, SystemFonts.DefaultFont,
                new Color(), 100, new Size(width, height));
            act.Should().Throw<ArgumentException>().WithMessage("Resolution must be positive");
        }
        
        [TestCase("Comic Sans")]
        [TestCase("qwerty")]
        [TestCase("default")]
        public void TagCloud_UnknownFont_Throws(string fontName)
        {
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, fontName,
                "Red", 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown Font *");
        }
        
        [TestCase("red")]
        [TestCase("graybol")]
        [TestCase("default")]
        public void TagCloud_UnknownColor_Throws(string colorName)
        {
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                colorName, 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown Color *");
        }
        
        [Test]
        public void TagCloud_SourceNotExist_Throws()
        {
            DeleteFiles();
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Source file not found");
        }
        
        [TestCase("doc")]
        [TestCase("docx")]
        [TestCase("pdf")]
        [TestCase("png")]
        [TestCase("xyz")]
        public void TagCloud_SourceWrongFormat_Throws(string extension)
        {
            DeleteFiles();
            sourceFile = random.Next() + "." + extension;
            CreateFile(sourceFile, DefaultContent);
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown source file format.");
        }
        
        [TestCase("txt")]
        public void TagCloud_SourceRightFormat_NotThrows(string extension)
        {
            DeleteFiles();
            sourceFile = random.Next() + "." + extension;
            CreateFile(sourceFile, DefaultContent);
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().NotThrow();
        }
        
        [Test]
        public void TagCloud_EmptySource_Throws()
        {
            DeleteFiles();
            CreateFile(sourceFile, "");
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Zero tags found");
        }
        
        [TestCase("doc")]
        [TestCase("docx")]
        [TestCase("pdf")]
        [TestCase("sas")]
        [TestCase("xyz")]
        public void TagCloud_ResultWrongFormat_Throws(string extension)
        {
            resultFile = random.Next() + "."  + extension;
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown image format");
        }
        
        [TestCase("png")]
        [TestCase("jpg")]
        [TestCase("jpeg")]
        [TestCase("tiff")]
        [TestCase("bmp")]
        public void TagCloud_ResultRightFormat_NotThrows(string extension)
        {
            resultFile = random.Next() + "."  + extension;
            Action act = () => tagCloud.CreateTagCloudFromFile(sourceFile, resultFile, "Comic Sans MS",
                "Red", 100, 100, 100);
            act.Should().NotThrow();
        }
    }
}