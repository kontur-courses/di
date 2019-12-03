using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudTextPreparation.Tests
{
    [TestFixture]
    public class TextPreparerTests
    {
        [Test]
        public void Constructor_Should_ThrowArgumentException_WhenConfigIsNull()
        {
            Action action = () => new TextPreparer(null);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void GetPreparedText_Should_ExcludeWords()
        {
            var excludedWords = new[] {"exclude", "this", "words"};
            
            var textPreparerConfig = new TextPreparerConfig().Excluding(excludedWords);
            var textPreparer = new TextPreparer(textPreparerConfig);
            var frequencyWords = textPreparer.GetWordsByFrequency(new[] {"exclude", "tag", "this", "words", "cloud"});
            
            frequencyWords.Select(f => f.Word).Should().NotContain(excludedWords);
        }
        
        [Test]
        public void GetPreparedText_Should_ExcludeWordsOnlySpecifiedWords()
        {
            var excludedWords = new[] {"a", "b", "c"};
            
            var textPreparerConfig = new TextPreparerConfig().Excluding(excludedWords);
            var textPreparer = new TextPreparer(textPreparerConfig);
            var frequencyWords = textPreparer.GetWordsByFrequency(new[] {"a", "b", "c", "d", "e","f"});
            
            frequencyWords.Select(f => f.Word).Should().BeEquivalentTo("d","e","f");
        }
        
        [Test]
        public void GetPreparedText_Should_ChangeWordsCaseToLower()
        {
            var textPreparer = new TextPreparer(new TextPreparerConfig());
            var frequencyWords = textPreparer.GetWordsByFrequency(new[] {"CHANGE","Case","ToLower"});

            frequencyWords.Select(f => f.Word).Should().BeEquivalentTo("change", "case", "tolower");
        }
        
        [Test]
        public void GetPreparedText_Should_Not_ContainRepeatedWords()
        {
            var textPreparer = new TextPreparer(new TextPreparerConfig());
            var frequencyWords = textPreparer.GetWordsByFrequency(new[] {"words","words","words"});

            frequencyWords.Select(f => f.Word).Should().BeEquivalentTo("words");
        }
        
        [Test]
        public void GetPreparedText_Should_CountWordsOccurrences()
        {
            var textPreparer = new TextPreparer(new TextPreparerConfig());
            var frequencyWords = textPreparer.GetWordsByFrequency(new[] {"cat","cat","dog","dog","dog"});

            frequencyWords
                .Should()
                .BeEquivalentTo(new FrequencyWord("cat", 2), new FrequencyWord("dog", 3));
        }
    }
}