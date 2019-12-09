using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextPreparation.Formatters;

namespace TagsCloudTextPreparation.Tests
{
    [TestFixture]
    public class WordsFormatterLowerCaseAndTrimTests
    {
        [Test]
        public void Format_Should_ChangeWordsCaseToLower()
        {
            var inputWords = new[] {"CHANGE", "Case", "ToLower"};
            
            var formattedWords =  new WordsFormatterLowercaseAndTrim().Format(inputWords);
            
            formattedWords.Should().BeEquivalentTo("change", "case", "tolower");
        }
        
        [Test]
        public void Format_Should_TrimWords()
        {
            var inputWords = new[] {"cat", "cat ", " cat", " cat "};
            
            var formattedWords =  new WordsFormatterLowercaseAndTrim().Format(inputWords);
            
            formattedWords.Should().BeEquivalentTo("cat", "cat", "cat", "cat");
        }
    }
}