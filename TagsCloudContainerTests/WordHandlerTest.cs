using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    public class WordHandlerTest
    {
        private Settings _settings;
        
        [SetUp]
        public void SetUp()
        {
            _settings = new Settings()
            {
                WordFontName = "Arial",
                WordFontSize = 16
            };
        }

        [TestCase("OneCharacters.txt")]
        [TestCase("AllWordsAreBoring.txt")]
        public void ProcessWords_ShouldReturnEmptyArray(string fileName)
        {
            var handler = new WordHandler(fileName);
            var words = handler.ProcessWords(_settings);
            words.Should().BeEmpty();
        }
    }
}