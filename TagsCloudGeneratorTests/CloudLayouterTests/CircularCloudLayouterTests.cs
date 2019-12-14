using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.CloudLayouter;
using TagsCloudGenerator.Tools;
using TagsCloudGeneratorTests.ToolsForTests;

namespace TagsCloudGeneratorTests.CloudLayouterTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private readonly Point center = new Point(7, 11);
        private CircularCloudLayouter layouter;
        private readonly Font font = SystemFonts.DefaultFont;

        private readonly Dictionary<string, int> wordToCount =
            new Dictionary<string, int> {["fish"] = 2, ["sun"] = 1, ["sofa"] = 1, ["cat"] = 40};

        [SetUp]
        public void SetUp()
        {
            var spiralGenerator = new SpiralGenerator(center, 0.5, Math.PI / 16);
            layouter = new CircularCloudLayouter(center, spiralGenerator);
        }

        [Test]
        public void GetTagsCloudCenter_ShouldReturnRightPoint()
        {
            var actual = layouter.LayoutWords(wordToCount, font);

            actual.Center.Should().Be(center);
        }

        [Test]
        public void LayoutWords_OnlyOneWord_CloudShouldContainsThisWord()
        {
            var word = "text";
            var cloud = layouter.LayoutWords(new Dictionary<string, int> {[word] = 1}, font);

            var actual = cloud.Words.First().Value;

            actual.Should().BeEquivalentTo(word);
        }

        [Test]
        public void LayoutWords_ShouldNotIntersection()
        {
            var words = layouter.LayoutWords(wordToCount, font).Words;

            words.All(w => words.Count(y => y.Rectangle.IntersectsWith(w.Rectangle)) == 1).Should().BeTrue();
        }

        [Test]
        public void LayoutWords_TwoRectanglesWithEqualsSize_RectanglesShouldNotIntersects()
        {
            var cloud = layouter.LayoutWords(new Dictionary<string, int> {["fish"] = 1, ["cat"] = 1}, font);

            var first = cloud.Words[0].Rectangle;
            var second = cloud.Words[1].Rectangle;

            first.IntersectsWith(second).Should().BeFalse();
        }

        [Test]
        public void LayoutWords_CloudShouldContainsNWords()
        {
            var cloud = layouter.LayoutWords(wordToCount, font);
            var actualWords = cloud.Words.Select(x => x.Value).ToList();

            actualWords.Should().BeEquivalentTo(wordToCount.Keys);
        }

        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(200)]
        public void LayoutWords_DistanceOfAdjacentRectanglesShouldNotExceedN(int wordsCount)
        {
            var manyWordToCount = CreateWordToCount(wordsCount);

            var words = layouter.LayoutWords(manyWordToCount, font).Words;

            foreach (var word in words)
            {
                words
                    .Count(other =>
                        word.Rectangle.Distance(other.Rectangle) <
                        Math.Max(word.Rectangle.Width, word.Rectangle.Height))
                    .Should()
                    .BeGreaterOrEqualTo(3);
            }
        }

        [TestCase(15)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        public void LayoutWords_AfterPutNRectangles_TheyShouldBeTightlySpaced(int wordsCount)
        {
            var manyWordsToCount = CreateWordToCount(wordsCount);

            var words = layouter.LayoutWords(manyWordsToCount, font).Words;

            var rectangles = words.Select(x => x.Rectangle).ToList();
            var hull = GeometryHelper.BuildConvexHull(rectangles);
            var cloudSquare = GeometryHelper.GetSquareOfConvexHull(hull);
            var rectanglesSquare = rectangles.Sum(x => x.Width * x.Height);

            var ration = cloudSquare / rectanglesSquare;

            ration.Should().BeLessOrEqualTo(1.5);
            Console.WriteLine(ration);
        }

        private static Dictionary<string, int> CreateWordToCount(int wordsCount)
        {
            var words = new Dictionary<string, int>();

            for (var i = 0; i < wordsCount; i++)
            {
                words.Add(i.ToString(), 1);
            }

            return words;
        }
    }
}