using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Visualization.Preprocessors;

namespace CloudTagVisualizer.Tests.PreprocessorsTests
{
    [TestFixture]
    public class RemovingBoringWordsPreprocessor_Should
    {
        private IHunspeller hunspeller;

        [SetUp]
        public void OnSetUp()
        {
            hunspeller = A.Fake<IHunspeller>();
        }
        
        [Test]
        public void Throw_WhenNullHunspellerGiven()
        {
            Action action = () => new RemovingBoringWordsPreprocessor(null);

            action.Should().Throw<ArgumentException>();
        }

        
        [TestCaseSource(typeof(RemovingBoringWordsTestCases), nameof(RemovingBoringWordsTestCases.SameValueCases))]
        public void ReturnSameArray_WhenHunspellerAlwaysReturnTrue(string[] words)
        {
            var preprocessor = new RemovingBoringWordsPreprocessor(hunspeller);
            A.CallTo(() => hunspeller.Check(null))
                .WithAnyArguments()
                .Returns(true);
            
            var result = preprocessor.Preprocess(words);

            result.Should().BeEquivalentTo(words);
        }

        [Test]
        public void NotReturnExactWord_WhenHunspellerCheckReturnedFalse()
        {
            var preprocessor = new RemovingBoringWordsPreprocessor(hunspeller);
            var words = new[] {"abc", "def", "qwe"};
            var expected = new[] {words[0], words[2]};
            A.CallTo(() => hunspeller.Check(words[0])).Returns(true);
            A.CallTo(() => hunspeller.Check(words[1])).Returns(false);
            A.CallTo(() => hunspeller.Check(words[2])).Returns(true);

            var result = preprocessor.Preprocess(words);

            result.Should().BeEquivalentTo(expected);
        }
    }
    
    internal class RemovingBoringWordsTestCases
    {
        public static object[] SameValueCases =
        {
            new object[] {Array.Empty<string>()},
            new object[] {new[] {"abc", "def", "qwe"}}
        };
    }
}