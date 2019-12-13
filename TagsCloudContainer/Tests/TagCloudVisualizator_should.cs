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

        [SetUp]
        public void SetUp()
        {
            tokensParser = A.Fake<ITokensParser>();
            filter = A.Fake<IFilter>();
            rectangleGenerator = A.Fake<IRectangleGenerator>();
            tagCloudVisualizator = new TagCloudVisualizator(tokensParser, filter, rectangleGenerator);
        }

        [Test]
        public void DrawTagCloud_ShouldFiltering()
        {
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => filter.Filtering(A<IEnumerable<string>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void DrawTagCloud_ShouldParsing()
        {
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => tokensParser.GetTokens(A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void DoSomething_WhenSomething()
        {
            var filteringWord = new[] {"aba", "abc"};
            A.CallTo(() => filter.Filtering(A<IEnumerable<string>>.Ignored)).ReturnsLazily(() => filteringWord);
            tagCloudVisualizator.DrawTagCloud("", TagsCloudSetting.GetDefault());
            A.CallTo(() => rectangleGenerator.PutNextRectangle(A<Size>.Ignored))
                .MustHaveHappened(filteringWord.Length, Times.Exactly);
        }
    }
}