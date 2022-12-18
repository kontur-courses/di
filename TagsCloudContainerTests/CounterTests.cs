using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CounterNamespace;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CounterTests
    {
        [SetUp]
        public void SetUp()
        {
            var filePath = Path.Combine(Utilities.ProjectPath, "TestWords.txt");
            words = File.ReadAllLines(filePath);
            repeatedWords = words
                .SelectMany((word, i) => Enumerable.Repeat(word, words.Length - i))
                .ToArray();
            counter = new Counter<string>(repeatedWords);
        }

        private string[] words;
        private string[] repeatedWords;
        private Counter<string> counter;

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetMostPopular_ShouldReturnsCorrectly(int count)
        {
            counter.GetMostPopular(count).Count().Should().Be(count);
            counter.GetMostPopular(count).Should().BeEquivalentTo(words.Take(count));
        }

        [TestCase("Место", ExpectedResult = 0)]
        [TestCase("Слово", ExpectedResult = 1)]
        [TestCase("День", ExpectedResult = 5)]
        [TestCase("Год", ExpectedResult = 10)]
        public int GetAmount_ShouldReturnsCorrectly(string word)
        {
            return counter.GetAmount(word);
        }
    }
}