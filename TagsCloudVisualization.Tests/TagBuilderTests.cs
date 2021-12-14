using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.Tags;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagBuilderTests
    {
        private TagBuilder tagBuilder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            tagBuilder = new TagBuilder();
        }

        [TestCase("облако", 1, 1)]
        [TestCase("Ленин", 3, 1)]
        [TestCase("nag1bat0r", 1, 10)]
        [DefaultFloatingPointTolerance(0.0001)]
        public void GetTags_ShouldCorrectCountTagWeight_ForSingleWord(string input, int inputCount, int numOfLines)
        {
            var fakeWordStatistic = new Dictionary<string, int> {{input, inputCount}};

            // var actual = tagBuilder.GetTags(string.Empty).ToList();
            //
            // actual.Should().ContainSingle(tag => tag.Weight == 1);
        }

        [Test]
        [DefaultFloatingPointTolerance(0.0001)]
        public void GetTags_ShouldCorrectCountTagWeight_ForMultipleWords()
        {
            const int numOfLines = 10;
            var fakeWordStatistic = new Dictionary<string, int>
            {
                {"облако", 1},
                {"Ленин", 3},
                {"nag1bat0r", 1}
            };

            // var actual = tagBuilder.GetTags(string.Empty).ToList();
            //
            // actual.Should().Contain(tag => tag.Text == "облако" && tag.Weight == (float) 1 / 5);
            // actual.Should().Contain(tag => tag.Text == "Ленин" && tag.Weight == (float) 3 / 5);
            // actual.Should().Contain(tag => tag.Text == "nag1bat0r" && tag.Weight == (float) 1 / 5);
        }
    }
}