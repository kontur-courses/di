using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagsCloudVisualization.Common.Stemers;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class HunspellStemerTests
    {
        private const string DictRuAff = @"dicts\ru.aff";
        private const string DictRuDic = @"dicts\ru.dic";
        private HunspellStemer stemer;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var hunspell = new Hunspell(DictRuAff, DictRuDic);
            stemer = new HunspellStemer(hunspell);
        }
        
        [TestCase("облако", "облако", TestName = "InputEqualStem")]
        [TestCase("облаком", "облако", TestName = "StemCheck")]
        [TestCase("облака", "облако", TestName = "InputIsPlural")]
        [TestCase("облаками", "облако", TestName = "PluralStemCheck")]
        public void TryGetStem_ShouldStemWords(string input, string expected)
        {
            var result = stemer.TryGetStem(input, out var actual);
            actual.Should().Be(expected);
            result.Should().BeTrue();
        }
        
        [TestCase("Ленин", TestName = "RussianProperName")]
        [TestCase("nag1bat0r", TestName = "SteamNickname")]
        public void TryGetStem_ShouldReturnFalse_WhenStemFailed(string input)
        {
            var result = stemer.TryGetStem(input, out var actual);
            result.Should().BeFalse();
        }
    }
}