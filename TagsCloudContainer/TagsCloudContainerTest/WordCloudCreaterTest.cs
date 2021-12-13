using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;
using TagsCloudContainer;
using System.Drawing;

namespace TagsCloudContainerTest
{
    public class WordCloudCreaterTest
    {
        private ICloudLayouter fakeCloudLayouter;
        private IWordsContainer fakeWordsContainer;
        private ImageSettings fakeImageSettings;

        [SetUp]
        public void InitializeServices()
        {
            fakeCloudLayouter = A.Fake<ICloudLayouter>();
            fakeWordsContainer = A.Fake<IWordsContainer>();
            fakeImageSettings = new ImageSettings
                (
                    new Size(1, 1),
                    FontFamily.GenericSansSerif,
                    Color.White,
                    Color.Black
                );
        }

        [Test]
        public void CheckCallsGetWordCloud()
        {
            var cloudCreator = new WordCloudCreator(fakeCloudLayouter, fakeWordsContainer);

            foreach (var e in cloudCreator.GetWordCloud(Graphics.FromImage(new Bitmap(1, 1)), fakeImageSettings))
            {
            }

            A.CallTo(() => fakeWordsContainer.GetWords()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CountCallsGetWordCloudCountShouldBeEqualsRectanglesCount()
        {
            var words = new Dictionary<string, int>()
            {
                {"a", 1},
                {"b", 2},
                {"c", 3}
            };
            A.CallTo(() => fakeWordsContainer.GetWords()).Returns(words);
            var cloudCreator = new WordCloudCreator(fakeCloudLayouter, fakeWordsContainer);

            foreach (var e in cloudCreator.GetWordCloud(Graphics.FromImage(new Bitmap(20, 20)), fakeImageSettings))
            {
            }

            A.CallTo(() => fakeCloudLayouter.PutNextRectangle(new Size(1,1)))
                .WithAnyArguments()
                .MustHaveHappened(words.Count, Times.Exactly);
        }
    }
}
