using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    public class ResultRailwayTests
    {
        private TagCloud tagCloud;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tagCloud = AutofacConfigurator.InjectWith<TagCloud>();
        }
        
        [Test]
        public void ShouldBeSuccessTest()
        {
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData());
            
            actual.IsSuccess.Should().BeTrue(actual.Error);
        }
        
        [Test]
        public void ShouldBeNotSuccessWhenIncorrectTextFilePath()
        {
            var config = GetConfigWithCorrectData();
            config.IgnoreFilePath = "incorrect text filepath";
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData());
            
            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"Can't read {config.TextFilePath} for some reason");
        }
        
        [Test]
        public void ShouldBeNotSuccessWhenIncorrectIgnoreFilePath()
        {
            var config = GetConfigWithCorrectData();
            config.TextFilePath = "incorrect text filepath";
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData());
            
            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"Can't read {config.TextFilePath} for some reason");
        }
        
        [Test]
        public void ShouldBeNotSuccessWhenIncorrectFontName()
        {
            var config = GetConfigWithCorrectData();
            config.Font = "incorrect font";
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData());
            
            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"Can't find font {config.Font}");
        }
        
        [Test]
        public void ShouldBeNotSuccessWhenNoWordsToStatistic()
        {
            var config = GetConfigWithCorrectData();
            config.MinWordToStatisticLength = byte.MaxValue;
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData());
            
            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"No words in statistic");
        }

        [Test]
        public void AprovelTest()
        {
            var expectedBitmap = new Bitmap("../../aprovel bmp.png").ToBytes();
            var actual = tagCloud.GetBitmap(GetConfigWithCorrectData()).GetValueOrThrow()!.ToBytes();

            actual.Should().BeEquivalentTo(expectedBitmap);
        }

        private static Config GetConfigWithCorrectData()
        {
            return new Config
            {
                SourceTextInterpretationMode = SourceTextInterpretationMode.LiteraryText,
                TagCloudResultActions = TagCloudResultActions.None,
                TextFilePath = "sample.txt",
                Font = "Arial",
                MinWordToStatisticLength = 3,
                Density = 5,
                WordCountToStatistic = 50,
                Color = Color.Aqua,
                SavePath = @"..\..\",
                SaveFileName = "aprovel bmp",
                Size = new Size(900, 900),
            };
        }
    }
}