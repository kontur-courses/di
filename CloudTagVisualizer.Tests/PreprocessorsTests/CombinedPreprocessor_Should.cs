using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Visualization.Preprocessors;

namespace CloudTagVisualizer.Tests.PreprocessorsTests
{
    public class CombinedPreprocessor_Should
    {
        private IWordsPreprocessor preprocessor1;
        private IWordsPreprocessor preprocessor2;
        private CombinedPreprocessor combinedPreprocessor;

        [SetUp]
        public void OnSetUp()
        {
            preprocessor1 = A.Fake<IWordsPreprocessor>();
            preprocessor2 = A.Fake<IWordsPreprocessor>();
            combinedPreprocessor = new CombinedPreprocessor(new[] {preprocessor1, preprocessor2});
        }

        [Test]
        public void OperateInGivenOrder()
        {
            var initialWords = new[] {"a", "b"};
            var resultAfterFirstPreprocessing = new[] {"aa", "bb"};
            var resultAfterSecondPreprocessing = new[] {"aa_aa", "bb_bb"};
            
            A.CallTo(() => preprocessor1.Preprocess(initialWords))
                .Returns(resultAfterFirstPreprocessing);
            A.CallTo(() => preprocessor2.Preprocess(resultAfterFirstPreprocessing))
                .Returns(resultAfterSecondPreprocessing);

            var result = combinedPreprocessor.Preprocess(initialWords);
            result.Should().BeEquivalentTo(resultAfterSecondPreprocessing);
        }
    }
}