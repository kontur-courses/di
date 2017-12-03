using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace TagsCloudVisualization
{
    public class TagMaker : ITagMaker
    {
        private readonly ICloudLayouter layouter;
        private readonly IFontSizeMaker fontSizeMaker;
        private readonly string fontFamily;

        public TagMaker(ICloudLayouter layouter, IFontSizeMaker fontSizeMaker, string fontFamily)
        {
            this.layouter = layouter;
            this.fontSizeMaker = fontSizeMaker;
            this.fontFamily = fontFamily;
        }

        public Dictionary<Rectangle, (string, Font)> MakeTagRectangles(
            Dictionary<string, int> frequencyDict)
        {
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();
            var maxfreq = frequencyDict.Values.Max();

            foreach (var word in frequencyDict)
            {
                var font = new Font(new FontFamily(fontFamily), fontSizeMaker.GetFontSizeByFreq(maxfreq, word.Value),
                    FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key, font);
                tagsDict.Add(layouter.PutNextRectangle(tagSize), (word.Key, font));
            }
            return tagsDict;
        }
    }

    [TestFixture]
    public class TagMaker_Should
    {
        [Test]
        public void DoSomething_WhenSomething()
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
}