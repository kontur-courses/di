using NUnit.Framework;
using System.Collections.Generic;
using TagCloud2;
using FakeItEasy;
using TagCloudVisualisation;
using System.Drawing;
using TagCloud2.TextGeometry;
using TagCloud2.Cloud;
using FluentAssertions;

namespace TagCloud2Tests
{
    public class ColoredCloudTests
    {
        private readonly ICloudLayouter cloud = A.Fake<ICloudLayouter>();

        private IColoredCloud coloredCloud = new ColoredCloud();

        private readonly IColoringAlgorithm whiteAlgo = new WhiteColoringAlgorithm();

        private readonly Color white = Color.FromArgb(255, 255, 255);

        private readonly Font font = SystemFonts.DefaultFont;

        [SetUp]
        public void SetUp()
        {
            coloredCloud = new ColoredCloud();
        }

        [Test]
        public void GetFromCloudLayouter_OnEmpty_ShouldWorkCorrect()
        {
            A.CallTo(() => cloud.GetRectangles()).Returns(System.Array.Empty<Rectangle>());
            coloredCloud.AddColoredWordsFromCloudLayouter(System.Array.Empty<IColoredSizedWord>(), cloud, whiteAlgo);
            coloredCloud.ColoredWords.Should().BeEmpty();
        }

        [Test]
        public void GetFromCloudLayouter_OnOneWord_ShouldWorkCorrect()
        {
            var rect = new Rectangle(0, 0, 1, 1);
            A.CallTo(() => cloud.GetRectangles()).Returns(new Rectangle[] { rect });
            var coloredSizedWord = new ColoredSizedWord("aboba", font);
            coloredCloud.AddColoredWordsFromCloudLayouter(new ColoredSizedWord[] { coloredSizedWord },
                cloud, whiteAlgo);

            var expectedWord = new ColoredSizedWord(white, rect, "aboba", font);

            coloredCloud.ColoredWords.Should().BeEquivalentTo(new ColoredSizedWord[] { expectedWord });
        }

        [Test]
        public void GetFromCloudLayouter_OnTwoWords_ShouldWorkCorrect()
        {
            var rect1 = new Rectangle(0, 0, 1, 1);
            var rect2 = new Rectangle(2, 2, 2, 2);
            A.CallTo(() => cloud.GetRectangles()).Returns(new Rectangle[] { rect1, rect2 });
            var coloredSizedWord1 = new ColoredSizedWord("aboba1", font);
            var coloredSizedWord2 = new ColoredSizedWord("aboba2", font);
            coloredCloud.AddColoredWordsFromCloudLayouter(new ColoredSizedWord[] 
            { 
                coloredSizedWord1,
                coloredSizedWord2
            }, cloud, whiteAlgo);

            var expList = new List<ColoredSizedWord>
            {
                new ColoredSizedWord(white, rect1, "aboba1", font),
                new ColoredSizedWord(white, rect2, "aboba2", font)
            };

            coloredCloud.ColoredWords.Should().BeEquivalentTo(expList);
        }
    }
}
