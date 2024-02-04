using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TagsCloudContainer;
using TagsCloudContainer.TextTools;

namespace TagsCloudTests
{
    [TestFixture]
    public class FileReadingTests
    {
        private ServiceProvider serviceProvider;
        private string tempFilePath;
        private const string testText = "Expected text from the file";

        private string CreateTempFile()
        {
            var tempFile = Path.GetTempFileName();
            using (var streamWriter = new StreamWriter(tempFile, false, Encoding.UTF8))
            {
                streamWriter.Write(testText);
            }
            return tempFile;
        }

        [SetUp]
        public void Setup()
        {
            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            serviceProvider = services.BuildServiceProvider();
            tempFilePath = CreateTempFile();
        }

        [Test]
        public void ReadTextFromFile_ShouldReturnCorrectText()
        {
            // Arrange
            var filePath = tempFilePath;
            var expectedText = testText;
            var sut = serviceProvider.GetRequiredService<ITextReader>();

            // Act
            var actualText = sut.ReadText(filePath);

            // Assert
            actualText.Should().Be(expectedText);
        }

        [Test]
        public void ReadTextFromFile_ShouldThrowIOException_WhenFileDoesNotExist()
        {
            // Arrange
            var filePath = "nonexistent_file.txt";
            var sut = serviceProvider.GetRequiredService<ITextReader>();

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => sut.ReadText(filePath));
        }
    }
}