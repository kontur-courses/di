using System.Collections.Generic;
using System.Drawing;
using FakeItEasy;
using NUnit.Framework;
using TagsCloudContainer.Filters;
using TagsCloudContainer.RectangleGenerator;
using TagsCloudContainer.TokensGenerator;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TagCloudVisualizator_Should
    {
        private ITokensParser tokensParser;
        private IFilter filter;
        private IRectangleGenerator rectangleGenerator;
        private TagCloudVisualizator tagCloudVisualizator;
        private IVisualizer visualizer;

        [SetUp]
        public void SetUp()
        {
            tokensParser = A.Fake<ITokensParser>();
            filter = A.Fake<IFilter>();
            rectangleGenerator = A.Fake<IRectangleGenerator>();
            visualizer = A.Fake<IVisualizer>();
            tagCloudVisualizator = new TagCloudVisualizator(tokensParser, filter, rectangleGenerator, visualizer);
        }

        [Test]
        public void DrawTagCloud_ShouldFiltering()
        {
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => filter.Filtering(A<IEnumerable<string>>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void DrawTagCloud_ShouldParsing()
        {
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => tokensParser.GetTokens(A<string>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void DrawTagCloud_ShouldPutRectangle()
        {
            var filteringWord = new[] {"aba", "abc"};
            A.CallTo(() => filter.Filtering(A<IEnumerable<string>>.Ignored)).Returns(filteringWord);
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => rectangleGenerator.PutNextRectangle(A<Size>.Ignored))
                .MustHaveHappened(filteringWord.Length, Times.Exactly);
        }

        [Test]
        public void DrawTagCloud_ShouldCorrectWorkVisualizer()
        {
            var filteringWord = new[] {"aba", "abc"};
            A.CallTo(() => filter.Filtering(A<IEnumerable<string>>.Ignored)).Returns(filteringWord);
            var rectangles = new[] {new Rectangle(0, 0, 50, 50), new Rectangle(50, 50, 50, 50)};
            A.CallTo(() => rectangleGenerator.PutNextRectangle(A<Size>.Ignored)).Returns(rectangles[1]).Once();
            A.CallTo(() => rectangleGenerator.PutNextRectangle(A<Size>.Ignored)).Returns(rectangles[0]).Once();

            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());

            A.CallTo(() => visualizer.DrawTag(A<TagRectangle>.That.Matches(x => x.Equals(new TagRectangle(filteringWord[0],rectangles[0]))), A<Font>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => visualizer.DrawTag(A<TagRectangle>.That.Matches(x => x.Equals(new TagRectangle(filteringWord[1],rectangles[1]))), A<Font>.Ignored))
                .MustHaveHappenedOnceExactly();
        }
    }
}