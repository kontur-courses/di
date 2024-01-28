using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
    }
}