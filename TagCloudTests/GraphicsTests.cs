using System.Collections.Generic;
using System.Drawing;
using Castle.Windsor;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;
using TagCloud;
using TagCloud.Models;

namespace TagCloudTests
{
    [TestFixture]
    public class GraphicsTests
    {
        private Graphics graphics;
        private WindsorContainer container;
        private string pathToBoringWords;
        private string pathToReadWords;
        private ImageSettings imageSettings;

        [SetUp]
        public void SetUp()
        {
            graphics = Graphics.FromImage(new Bitmap(300,300));
            container = TagCloud.Program.GetContainer();
            pathToReadWords = SetUpMethods.GetPathToWordsToRead();
            pathToBoringWords = SetUpMethods.GetPathToBoringWords();
            SetUpMethods.CreateFile(pathToReadWords);
            SetUpMethods.WriteLinesInFile(pathToReadWords, "i", "I", "i", "I", "Boat", "BoaT", "BoAt", "BOAT", "in", "IN");
            SetUpMethods.CreateFile(pathToBoringWords);
            SetUpMethods.WriteLinesInFile(pathToBoringWords,"in");
            imageSettings = new ImageSettings(300, 300, "Arial", "ShadesOfPink");
        }

        [Test]
        public void ICloud_Should_MakeTagRectangleCorrectly()
        {
            var cloud = container.Resolve<ICloud>();
            var font = new Font("Arial", 24, FontStyle.Bold);
            var tagCollection = cloud.GetRectangles(graphics, imageSettings, pathToReadWords);
            var expectedCollection = new List<TagRectangle>()
            {
                new TagRectangle(new Tag("i",4,font), new RectangleF()),
                new TagRectangle(new Tag("boat",4,font), new RectangleF())
            };
            tagCollection.Should().BeEquivalentTo(expectedCollection,
                options => options.Excluding(x => x.SelectedMemberInfo.Name == "Area"));
        }
    }
}
