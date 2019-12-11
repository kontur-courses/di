using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.Models;

namespace TagCloudTests
{
    [TestFixture]
    public class CloudTests
    {
        [SetUp]
        public void SetUp()
        {
            graphics = Graphics.FromImage(new Bitmap(300, 300));
            font = new Font("Arial", 24, FontStyle.Bold);
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

        private Font font;
        private Graphics graphics;
        private WindsorContainer container;
        private string pathToBoringWords;
        private string pathToReadWords;
        private ImageSettings imageSettings;

        [Test]
        public void ICloud_Should_MakeTagRectanglesCorrectly()
        {
            var cloud = container.Resolve<ICloud>();
            var tagCollection = cloud.GetRectangles(graphics, imageSettings, pathToReadWords);
            var expectedCollection = new List<TagRectangle>
            {
                new TagRectangle(new Tag("i", 4, font), new RectangleF()),
                new TagRectangle(new Tag("boat", 4, font), new RectangleF())
            };
            tagCollection.Should().BeEquivalentTo(expectedCollection,
                options => options.Excluding(x => x.SelectedMemberInfo.Name == "Area"));
        }

        [Test]
        public void ICloud_Should_ThrowsException_When_UncorrectPath()
        {
            var cloud = container.Resolve<ICloud>();
            Action action = () => cloud.GetRectangles(graphics, imageSettings, "aaa");
            action.Should().Throw<FileNotFoundException>("Файла не существует");
        }
    }
}