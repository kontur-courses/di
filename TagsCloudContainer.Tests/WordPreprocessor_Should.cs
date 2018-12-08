using FluentAssertions;
using MyStemAdapter;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    public class WordPreprocessor_Should
    {
        private WordPreprocessor wordPreprocessor;

        [SetUp]
        public void SetUp()
        {
            wordPreprocessor = new WordPreprocessor(new FilterHashSet<PartOfSpeech>(FilterType.BlackList)
                {PartOfSpeech.Conjecture});
        }

        [Test]
        public void ExtractStems()
        {
            wordPreprocessor.PreprocessWords(new[] {"стулья"}).Should().BeEquivalentTo("стул");
        }

        [Test]
        public void Filter_BlacklistPartsOfSpeech()
        {
            wordPreprocessor.PreprocessWords(new[] {"стул", "и"}).Should().BeEquivalentTo("стул");
        }

        [Test]
        public void SaveOrder()
        {
            wordPreprocessor.PreprocessWords(new[] {"стулья", "машины"}).Should().BeEquivalentTo("стул", "машина");
        }
    }
}
