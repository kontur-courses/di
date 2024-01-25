using TagsCloudContainer.Algorithm;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture;

namespace TagsCloudContainerTests
{

    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            var algSettings = new AlgorithmSettings();
            var imgSettings = new ImageSettings();
            layouter = new CircularCloudLayouter(algSettings, imgSettings);
        }

        [Test]
        public void CreateAnEmptyLayout_WhenFrequencyDictionaryIsEmpty()
        {
            var textRectangles = layouter.GetRectangles(new Dictionary<string, int>());

            textRectangles.Should().BeEmpty();
        }

        [Test]
        public void CreateLayoutWithoutIntersections_WhenNoIdenticalFrequencies()
        {
            var wordsFrequencies = new Dictionary<string, int>()
        {
            {"яблоко" , 1},
            {"банан", 2},
            {"груша", 6},
            {"мандарин", 17},
        };

            var textRectangles = layouter.GetRectangles(wordsFrequencies);

            IsIntersections(textRectangles).Should().BeFalse();
        }

        [Test]
        public void CreateLayoutWithoutIntersections_WhenIdenticalFrequencies()
        {
            var wordsFrequencies = new Dictionary<string, int>()
        {
            {"яблоко" , 1},
            {"банан", 6},
            {"груша", 6},
            {"мандарин", 17},
            {"апельсин", 17},

        };

            var textRectangles = layouter.GetRectangles(wordsFrequencies);

            IsIntersections(textRectangles).Should().BeFalse();
        }

        [Test]
        public void CreateLayout_WhenFrequencyWordIncreasesTheSizeRectangleIncreases()
        {
            var wordsFrequencies = new Dictionary<string, int>()
        {
            {"купец" , 1},
            {"упецк", 2},
            {"пецку", 6},
            {"ецкуп", 17},
        };

            var textRectangles = layouter.GetRectangles(wordsFrequencies.OrderBy(word => word.Value).ToDictionary());

            textRectangles.Should().BeInAscendingOrder(textRectangle => textRectangle.Area);
        }

        public bool IsIntersections(List<TextRectangle> textRectangles)
        {
            var rectList = textRectangles.Select(x => x.Rectangle).ToList();

            for (int i = 0; i < rectList.Count; i++)
                for (int j = i + 1; j < rectList.Count; j++)
                    if (rectList[i].IntersectsWith(rectList[j]))
                        return true;

            return false;
        }

    }
}
