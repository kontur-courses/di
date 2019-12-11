using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Algorithm.Layouting;
using TagsCloudContainer.Algorithm.Organizing;
using TagsCloudContainer.Algorithm.SizeProviding;
using TagsCloudContainer.Algorithm.WeightSetting;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudContainerTests
{
    public class LayoutAlgorithmTests
    {
        private LayoutAlgorithm layoutAlgorithm;

        [SetUp]
        public void SetUp()
        {
            layoutAlgorithm = new LayoutAlgorithm(new FrequencyWeightSetter(), new CorrespondingToWeightSizeProvider(),
                new WordOrganizerBySize(), new CircularLayouter());
        }


        [TestCaseSource(nameof(GetLayoutShouldReturnNonRepeatingWordsTestCases))]
        public void GetLayout_ShouldReturnNonRepeatingWords(List<string> words)
        {
            var pictureSize = new Size(100, 100);

            var layout = layoutAlgorithm.GetLayout(words, pictureSize);

            layout.Select(p => p.Item1).Should().BeEquivalentTo(words.Distinct());
        }

        private static IEnumerable GetLayoutShouldReturnNonRepeatingWordsTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "я", "я", "мы", "мы" }).SetName("2 unique words");
                yield return new TestCaseData(new List<string> { "я", "ты", "он", "она" }).SetName("all unique words");
                yield return new TestCaseData(Enumerable.Repeat("я", 50).ToList()).SetName("no unique words");
            }
        }

        [TestCaseSource(nameof(GetLayoutRectanglesTestCases))]
        public void GetLayout_ShouldReturnDisjointRectangles(List<string> words)
        {
            var pictureSize = new Size(100, 100);

            var layout = layoutAlgorithm.GetLayout(words, pictureSize);
            var rectangles = layout.Select(p => p.Item2).ToList();

            foreach (var rectangle in rectangles)
            {
                rectangles.Where(r => r != rectangle && r.IntersectsWith(rectangle)).Should().BeEmpty();
            }
        }

        private static IEnumerable GetLayoutRectanglesTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> { "я", "я", "мы", "мы" }).SetName("4 words");
                yield return new TestCaseData(Enumerable.Range(1, 100).Select(i => i.ToString()).ToList()).SetName(
                    "100 words");
            }
        }

        [TestCaseSource(nameof(GetLayoutRectanglesTestCases))]
        public void GetLayout_ShouldReturnRectanglesThatCloseToEachOther(List<string> words)
        {
            var pictureSize = new Size(100, 100);

            var layout = layoutAlgorithm.GetLayout(words, pictureSize);
            var rectangles = layout.Select(p => p.Item2).ToList();

            var maxDiagonal = rectangles.Max(r =>
                GeometryHelper.GetDistanceBetweenPoints(r.Location, new Point(r.Right, r.Bottom)));
            foreach (var rectangle in rectangles)
            {
                rectangles.Where(r => r != rectangle)
                    .Min(r => GeometryHelper.GetDistanceBetweenRectanglesCenters(r, rectangle))
                    .Should()
                    .BeLessOrEqualTo(maxDiagonal);
            }
        }
    }
}