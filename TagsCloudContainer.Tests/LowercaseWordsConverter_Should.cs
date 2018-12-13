using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Converter;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class LowercaseWordsConverter_Should
    {
        private LowercaseWordsConverter lowercaseWordsConverter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            lowercaseWordsConverter = new LowercaseWordsConverter();
        }

        [Test]
        public void Convert_EmptyCollection_ReturnEmptyCollection()
        {
            var words = new List<string>();

            lowercaseWordsConverter.Convert(words)
                .Should()
                .BeEmpty();
        }

        [Test]
        [TestCase("cUTE", "LOOSE", "tHrone", "palm", "baTTery", TestName = "first iteration")]
        [TestCase("paralLel", "A", "b", "NoRM", "FOrUM", TestName = "second iteration")]
        [TestCase("sailoR", "fAT", "leaD", "continiouS", "TRAy", TestName = "third iteration")]
        public void Convert_Correctly(params string[] words)
        {
            var convertedWords = lowercaseWordsConverter.Convert(words);
            using (ApprovalTests.Namers.ApprovalResults.ForScenario(words))
                Approvals.Verify(string.Join(" ", convertedWords));
        }
    }
}