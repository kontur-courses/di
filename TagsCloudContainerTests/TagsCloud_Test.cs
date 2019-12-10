using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.ApplicationRunning;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudLayouters.CircularCloudLayouter;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.BitmapMakers;
using TagsCloudContainer.CloudVisualizers.ImageSaving;
using TagsCloudContainer.TextParsing.CloudParsing;
using TagsCloudContainer.TextParsing.CloudParsing.ParsingRules;
using TagsCloudContainer.TextParsing.FileWordsParsers;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TagsCloud_Test
    {
        private SettingsManager settings;
        private TagsCloud cloud;
        private string testFilePath;

        [SetUp]
        public void SetUp()
        {
            settings = new SettingsManager();
            var parser = new CloudWordsParser(() => settings.GetWordsParserSettings());
            var layouter = new CloudLayouter(() => settings.GetLayouterSettings());
            var visualizer = new CloudVisualizer(() => settings.GetVisualizerSettings());
            var saver = new ImageSaver(() => settings.GetImageSaverSettings());
            cloud = new TagsCloud(parser, layouter, visualizer, saver);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        private void MakeTestFile()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var filename = "test.txt";
            var fullpath = Path.Combine(currentPath, filename);
            using (var fs = File.Create(fullpath))
            {
                var info = new UTF8Encoding(true).GetBytes("some\n\rtest\n\rwords\n\rtest\n\rwords\n\rtest\n\rtest");
                fs.Write(info, 0, info.Length);
            }

            testFilePath = fullpath;
        }

        [Test]
        public void GenerateTagCloud_Should_ThrowInvalidOperationException_When_NoWordsParsed()
        {
            Following.Code(() => cloud.GenerateTagCloud()).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void ParseWords_Should_ParseWithNoExceptions_When_FileExists()
        {
            MakeTestFile();
            settings.ConfigureWordsParserSettings(new TxtWordParser(), testFilePath, new DefaultParsingRule());
            Following.Code(() => cloud.ParseWords()).ShouldNotThrow("file exists and is correct");
        }

        [Test]
        public void GenerateTagCloud_Should_GenerateWithNoExceptions_When_WordsAreParsed()
        {
            MakeTestFile();
            settings.ConfigureWordsParserSettings(new TxtWordParser(), testFilePath, new DefaultParsingRule());
            cloud.ParseWords();
            var algorithm = new CircularCloudLayouter(new Point(0, 0), 0.1, 1);
            settings.ConfigureLayouterSettings(algorithm, 100, 0.1, 1);
            Following.Code(() => cloud.GenerateTagCloud()).ShouldNotThrow("words are parsed");
        }

        [Test]
        public void GenerateTagCloud_Should_VisualizeCloudWithNoExceptions_When_CloudIsGenerated()
        {
            MakeTestFile();
            settings.ConfigureWordsParserSettings(new TxtWordParser(), testFilePath, new DefaultParsingRule());
            cloud.ParseWords();
            var algorithm = new CircularCloudLayouter(new Point(0, 0), 0.1, 1);
            settings.ConfigureLayouterSettings(algorithm, 100, 0.1, 1);
            cloud.GenerateTagCloud();
            var font = new Font("Arial", 16);
            settings.ConfigureVisualizerSettings(new Palette(), new DefaultBitmapMaker(), 700, 700, font);
            Following.Code(() => cloud.VisualizeCloud()).ShouldNotThrow("cloud is successfully generated");
        }

        [Test]
        public void GenerateTagCloud_Should_SaveWithNoExceptions_When_CloudIsVisualized()
        {
            MakeTestFile();
            settings.ConfigureWordsParserSettings(new TxtWordParser(), testFilePath, new DefaultParsingRule());
            cloud.ParseWords();
            var algorithm = new CircularCloudLayouter(new Point(0, 0), 0.1, 1);
            settings.ConfigureLayouterSettings(algorithm, 100, 0.1, 1);
            cloud.GenerateTagCloud();
            var font = new Font("Arial", 16);
            settings.ConfigureVisualizerSettings(new Palette(), new DefaultBitmapMaker(), 700, 700, font);
            cloud.VisualizeCloud();
            var currentPath = Directory.GetCurrentDirectory();
            var filename = "test.jpg";
            var fullpath = Path.Combine(currentPath, filename);
            settings.ConfigureImageSaverSettings(ImageFormat.Jpeg, fullpath);
            Following.Code(() => cloud.SaveVisualized()).ShouldNotThrow("all steps worked correctly");
            File.Delete(fullpath);
        }

        [Test]
        public void GenerateTagCloud_Should_SaveCreateImage_When_AllStepsAreCorrect()
        {
            MakeTestFile();
            settings.ConfigureWordsParserSettings(new TxtWordParser(), testFilePath, new DefaultParsingRule());
            cloud.ParseWords();
            var algorithm = new CircularCloudLayouter(new Point(0, 0), 0.1, 1);
            settings.ConfigureLayouterSettings(algorithm, 100, 0.1, 1);
            cloud.GenerateTagCloud();
            var font = new Font("Arial", 16);
            settings.ConfigureVisualizerSettings(new Palette(), new DefaultBitmapMaker(), 700, 700, font);
            cloud.VisualizeCloud();
            var currentPath = Directory.GetCurrentDirectory();
            var filename = "test.jpg";
            var fullpath = Path.Combine(currentPath, filename);
            settings.ConfigureImageSaverSettings(ImageFormat.Jpeg, fullpath);
            cloud.SaveVisualized();
            File.Exists(fullpath).Should().BeTrue("all steps worked correctly");
            File.Delete(fullpath);
        }
    }
}