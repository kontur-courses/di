using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Layouting;
using TagsCloudContainer.Sizing;
using TagsCloudContainer.TagsCloudGenerating;

/*
namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TagsCloudGeneratorTests
    {
        private readonly Size minLetterSize = new Size(16, 20);
        private readonly IWordsSizer wordsSizer = new FrequencyWordsSizer();
        private readonly Point center = new Point(300, 300);
        private ITagsCloudLayouter layouter;
        private static readonly Random Random = new Random();
        private TagsCloudGenerator generator;

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(center, new TagsCloudFactory());
            generator = new TagsCloudGenerator(new TagsCloudGeneratorSettings(minLetterSize, layouter, wordsSizer));
        }

        [Test]
        public void CreateCloud_CreatesWordsSizesProportionalToFrequency()
        {
            var words = new List<string>() {"aaa", "aaa", "aaa", "bb", "bb", "c"};
            var cloud = generator.CreateCloud(words);
            var wordsSizes = new Dictionary<string, Size>();
            foreach (var tCWord in cloud.AddedWords)
            {
                wordsSizes[tCWord.Word] = tCWord.Rectangle.Size;
            }

            wordsSizes["aaa"].Should().BeEquivalentTo(new Size(minLetterSize.Width * 3 * 3, minLetterSize.Height * 3));
            wordsSizes["bb"].Should().BeEquivalentTo(new Size(minLetterSize.Width * 2 * 2, minLetterSize.Height * 2));
            wordsSizes["c"].Should().BeEquivalentTo(new Size(minLetterSize.Width * 1 * 1, minLetterSize.Height * 1));
        }

        [Test]
        public void CreateCloud_CreatesCloudOfWordsInscribedInCircle()
        {
            var words = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                words.Add(RandomString(1));
                words.Add(RandomString(2));
                words.Add(RandomString(3));
                words.Add(RandomString(4));
            }

            var cloud = generator.CreateCloud(words);
            var totalSquare = cloud.AddedWords.Sum(x => x.Rectangle.Height * x.Rectangle.Width);
            foreach (var TCWord in cloud.AddedWords)
            {
                var r = Math.Sqrt(totalSquare / Math.PI);
                var dist = Math.Sqrt(Math.Pow((TCWord.Rectangle.X - center.X), 2) +
                                     Math.Pow((TCWord.Rectangle.Y - center.Y), 2));
                dist.Should().BeLessThan(r * 1.2);
            }
        }
    }
}
*/