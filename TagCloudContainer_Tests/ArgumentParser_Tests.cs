using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure.Common;

namespace TagCloudContainer_Tests
{
    public class Tests
    {
        private readonly ConsoleArgumentParser parser = new ConsoleArgumentParser();

        [Test]
        public void GetFilePath_IfGetFileName()
        {
            var args = new[] {"-f", "file", "-n", "test"};

            var actual = parser.GetPath(args);
            actual.Should().Be("file");
        }

        [Test]
        public void GetFilePath_IfNotGetRequiredFileName_ReturnNull()
        {
            var args = new[] {"-w", "140", "-h", "180"};

            var actual = parser.GetPath(args);

            actual.Should().BeNullOrEmpty();
        }


        [Test]
        public void GetWordSettings_IfGetFontName()
        {
            var args = new[] {"-f", "file", "-n", "test", "-o", "Universe"};

            var actual = parser.GetWordSetting(args);

            actual.FontName.Should().Be("Universe");
        }

        [Test]
        public void GetWordSettings_IfGetSetBrash()
        {
            var args = new[] {"-f", "file", "-n", "test", "-c", "Red"};

            var actual = parser.GetWordSetting(args);

            actual.Color.Should().Be("Red");
        }

        [Test]
        public void GerImageSetting_Width_Height()
        {
            var args = new[] {"-f", "file", "-n", "test", "-w", "180", "-h", "100"};

            var actual = parser.GetImageSetting(args);

            actual.Should().BeEquivalentTo(new ImageSetting(100, 180, "White", "png", "test"));
        }
    }
}