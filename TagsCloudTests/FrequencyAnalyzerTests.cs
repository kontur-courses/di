using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TagsCloudContainer;
using TagsCloudContainer.FrequencyAnalyzers;

namespace TagsCloudTests
{
    [TestFixture]
    public class FrequencyAnalyzerTests
    {
        private IAnalyzer sut;
        private ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            serviceProvider = services.BuildServiceProvider();
            sut = serviceProvider.GetRequiredService<IAnalyzer>();
        }

        [Test]
        public void Analyze_ShouldReturnCorrectFrequency_ForSimpleText()
        {
            // Arrange
            var text = "hello\nworld\nhello";
            var expectedFrequency = new List<(string, int)>
                {
                    ( "hello", 2 ),
                    ( "world", 1 )
                };

            // Act
            sut.Analyze(text);

            // Assert
            sut.GetAnalyzedText().Should().BeEquivalentTo(expectedFrequency);
        }

        [Test]
        public void Analyze_ShouldNotChangeFrequency_ForEmptyText()
        {
            // Arrange
            var text = string.Empty;
            var expectedFrequency = new List<(string, int)>();

            // Act
            sut.Analyze(text);

            // Assert
            sut.GetAnalyzedText().Should().BeEquivalentTo(expectedFrequency);
        }

        [Test]
        public void GetAnalyzedText_ShouldNotContainExcludedWord()
        {
            // Arrange
            var text = "hello\nworld\nhello";
            var exclude = "hello";


            // Act
            sut.Analyze(text, CreateTempFile(exclude));

            // Assert
            sut.GetAnalyzedText().Should().NotContain(x => x.Item1.Contains(exclude));
        }

        private string CreateTempFile(string text)
        {
            var tempFile = Path.GetTempFileName();
            using (var streamWriter = new StreamWriter(tempFile, false, Encoding.UTF8))
            {
                streamWriter.Write(text);
            }
            return tempFile;
        }
    }
}