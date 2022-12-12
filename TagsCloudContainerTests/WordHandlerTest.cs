using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    public class WordHandlerTest
    {
        private TxtReader _txtReader;
        
        [SetUp]
        public void SetUp()
        {
            _txtReader = new TxtReader();
        }

        [TestCase("OneCharacters.txt")]
        [TestCase("AllWordsAreBoring.txt")]
        [TestCase("OneCharacters.docx")]
        [TestCase("AllWordsAreBoring.docx")]
        public void ProcessWords_ShouldReturnEmptyArray(string fileName)
        {
            Settings settings = new Settings()
            {
                WordFontName = "Arial",
                WordFontSize = 16,
                FileName = fileName
            };
            WordHandler handler = null;
            if(fileName.Contains("docx"))
               handler = new WordHandler(new WordReader(), settings);
            if (fileName.Contains("txt"))
                handler = new WordHandler(new TxtReader(), settings);
            
            var words = handler.ProcessWords();
            words.Should().BeEmpty();
        }
    }
}