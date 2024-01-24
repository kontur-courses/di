using FluentAssertions;
using System.Text;
using TagsCloudContainer.FrequencyAnalyzers;

namespace TagsCloudTests
{
    internal class TextPreprocessorTests
    {
        //private string tempFile;
        private const string excludedWords = "stop\nword";
        private TextPreprocessing textPreprocessor;
        [SetUp]
        public void Setup()
        {
            textPreprocessor = new TextPreprocessing(CreateTempFile(excludedWords));
        }

        private string CreateTempFile(string content)
        {
            var tempFile = Path.GetTempFileName();
            using (var streamWriter = new StreamWriter(tempFile, false, Encoding.UTF8))
            {
                streamWriter.Write(content);
            }
            return tempFile;
        }

        [Test]
        public void Preprocess_ShouldExcludeCorrectWords()
        {
            // Arrange
            var inputText = "stop\nword\nexample\none";
            var expectedWords = new[] { "example", "one" };

            // Act
            var actualWords = new List<string>(textPreprocessor.Preprocess(inputText));

            // Assert
            actualWords.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void Preprocess_ShouldConvertToLowercase()
        {
            // Arrange
            var inputText = "Example\nONE";
            var expectedWords = new[] { "example", "one" };

            // Act
            var actualWords = textPreprocessor.Preprocess(inputText).ToList();

            // Assert
            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
