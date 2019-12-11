using System;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TextRetrieverTests
    {
        [Test]
        public void TextRetriever_ThrowsNullArgumentException_WhenPathIsNull()
        {
            Action action = () => TextRetriever.RetrieveTextFromFile(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TextRetriever_ThrowsArgumentException_WhenFileDoesNotExists()
        {
            Action action = () => TextRetriever.RetrieveTextFromFile("nonexistingpath");
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TextRetriever_ReturnsCorrectText_WhenFileIsTxt()
        {
            var textPath = TestContext.CurrentContext.TestDirectory + "\\Tests\\TestTexts\\animals.txt";
            var expectedText = File.ReadAllText(textPath, Encoding.UTF8);
            TextRetriever.RetrieveTextFromFile(textPath).Should().BeEquivalentTo(expectedText);
        }
    }
}