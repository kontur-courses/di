using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class Preprocessor_Should
    {
        [Test]
        public void Process_WordsToLowerCase()
        {
            var words = new List<string>
            {
                "tourIst",
                "SHOOT",
                "relative",
                "Gravity",
                "employee",
                "heRD",
                "nodE",
                "Screen",
                "launch",
                "sEe"
            };
            var expected = new List<string>
            {
                "tourist",
                "shoot",
                "relative",
                "gravity",
                "employee",
                "herd",
                "node",
                "screen",
                "launch",
                "see"
            };
            var fakeWordsReader = A.Fake<IWordsReader>();
            A.CallTo(() => fakeWordsReader.GetWords()).Returns(words);
            var preprocessor = new SimplePreprocessor(fakeWordsReader);

            preprocessor.Process()
                .ToList()
                .Should()
                .BeEquivalentTo(expected);
        }

        [Test]
        public void Process_ExcludingBoringWords()
        {
            var words = new List<string>
            {
                "a",
                "the",
                "with",
                "in",
                "of",
                "at",
                "from",
                "into",
                "he",
                "i",
                "she",
                "we",
                "they",
                "we",
                "ample",
                "defend",
                "public",
                "conscience",
                "forestry"
            };
            var expected = new List<string>
            {
                "ample",
                "defend",
                "public",
                "conscience",
                "forestry"
            };
            var fakeWordsReader = A.Fake<IWordsReader>();
            A.CallTo(() => fakeWordsReader.GetWords()).Returns(words);
            var preprocessor = new SimplePreprocessor(fakeWordsReader);

            preprocessor.Process()
                .ToList()
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}