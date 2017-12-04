using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class TagMaker_Mock
    {
        [Test]
        public void SimpleMockTest()
        {
            var fontSizeMakerMock = new Mock<IFontSizeMaker>();
            fontSizeMakerMock.Setup(x => x.GetFontSizeByFreq(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(80);

            var layouterMock = new Mock<ICloudLayouter>();
            layouterMock.Setup(x => x.PutNextRectangle(It.IsAny<Size>()))
                .Returns((Size size) => new Rectangle(new Point(10, 10), size));

            var frequencyDict = new Dictionary<string, int>()
            {
                {"test", 5}
            };

            var actualTag = new TagMaker(layouterMock.Object, fontSizeMakerMock.Object, "Tahoma")
                .MakeTagRectangles(frequencyDict).ToList()[0];

            actualTag.Key.Location.Should().Be(new Point(10, 10));
            actualTag.Value.Item1.Should().Be("test");
            actualTag.Value.Item2.Size.Should().Be(80);
        }

        
    }
}