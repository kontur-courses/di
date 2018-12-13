using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsCounter;

namespace TagsCloudContainer.Tests
{
    [TestFixture()]
    [UseReporter(typeof(DiffReporter))]
    public class SimpleWordCounter_Should
    {
        private SimpleWordsCounter wordsCounter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wordsCounter = new SimpleWordsCounter();
        }

        [Test]
        public void GetFrequency_EmptyCollection_ReturnEmptyCollection()
        {
            var words = new List<string>();

            wordsCounter.GetWordsFrequency(words)
                .Should()
                .BeEmpty();
        }

        [Test]
        [TestCase("library", "uncle", "attitude", "moment", "shopping", "library", "library", "uncle")]
        [TestCase("measurement", "studio", "heart", "response", "bath", "batch", "batch", "batch", "batch")]
        [TestCase("performance", "inflation", "injury", "statement", "perspective")]
        public void GetFrequency_Correctly(params string[] words)
        {
            var wordsFrequency = wordsCounter.GetWordsFrequency(words)
                .Select(pair => $"{pair.Key}:{pair.Value}");

            using (ApprovalTests.Namers.ApprovalResults.ForScenario(words))
                Approvals.Verify(string.Join(" ", wordsFrequency));
        }
    }
}