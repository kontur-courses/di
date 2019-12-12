using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.Formatters;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class FormatterLowerCaseAndTrimTests
    {
        [Test]
        public void Format_Should_ChangeWordsCaseToLower()
        {
            var inputWords = new[] {"CHANGE", "Case", "ToLower"};
            
            var formattedWords =  new FormatterLowercaseAndTrim().Format(inputWords);
            
            formattedWords.Should().BeEquivalentTo("change", "case", "tolower");
        }
        
        [Test]
        public void Format_Should_TrimWords()
        {
            var inputWords = new[] {"cat", "cat ", " cat", " cat "};
            
            var formattedWords =  new FormatterLowercaseAndTrim().Format(inputWords);
            
            formattedWords.Should().BeEquivalentTo("cat", "cat", "cat", "cat");
        }
    }
}