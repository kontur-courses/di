using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.Models;

namespace TagCloudTests
{
    [TestFixture]
    public class GraphicsTests
    {
        [SetUp]
        public void SetUp()
        {
            container = TagCloud.Program.GetContainer();
            pathToReadWords = SetUpMethods.GetPathToWordsToRead();
            pathToBoringWords = SetUpMethods.GetPathToBoringWords();
            SetUpMethods.CreateFile(pathToReadWords);
            SetUpMethods.WriteLinesInFile(pathToReadWords, "i", "I", "i", "I", "Boat", "BoaT", "BoAt", "BOAT", "in",
                "IN");
            SetUpMethods.CreateFile(pathToBoringWords);
            SetUpMethods.WriteLinesInFile(pathToBoringWords, "in");
            imageSettings = new ImageSettings(300, 300, "Arial", "ShadesOfPink");
        }

        private WindsorContainer container;
        private string pathToBoringWords;
        private string pathToReadWords;
        private ImageSettings imageSettings;

        public static class PaletteSource
        {
            private static readonly TestCaseData[] TestCases =
            {
                new TestCaseData(new Palette("TestBluePalette", Color.Aqua, Color.AliceBlue, Color.Azure)).SetName(
                    "TestBluePalette"),
                new TestCaseData(new Palette("TestRedPalette", Color.Red, Color.DarkRed, Color.IndianRed)).SetName(
                    "TestRedPalette")
            };
        }

        [Test]
        public void ICloudVisualization_Should_ThrowsException_When_UncorrectPath()
        {
            var cloudVisualization = container.Resolve<ICloudVisualization>();
            Action action = () => cloudVisualization.GetAndDrawRectangles(imageSettings, "aaa");
            action.Should().Throw<FileNotFoundException>("Файла не существует");
        }

        [Test]
        public void ICloudVisualization_Should_WorkCorrectly()
        {
            var cloudVisualization = container.Resolve<ICloudVisualization>();
            var image = cloudVisualization.GetAndDrawRectangles(imageSettings, pathToReadWords);
            image.Width.Should().Be(imageSettings.Width);
            image.Height.Should().Be(imageSettings.Height);
        }

        [Test]
        [TestCaseSource(typeof(PaletteSource), "TestCases")]
        public void RectanglesCustomizer_Should_CustomizeTagRectanglesCorrectly(Palette palette)
        {
            var tagRectangles = new List<TagRectangle>();
            for (var i = 0; i < 50; i++)
                tagRectangles.Add(new TagRectangle(new Tag("a", 4, null), new RectangleF()));
            RectanglesCustomizer.GetRectanglesWithPalette(palette, tagRectangles)
                .Any(r => !palette.Colors.Contains(r.Color))
                .Should().BeFalse();
        }
    }
}