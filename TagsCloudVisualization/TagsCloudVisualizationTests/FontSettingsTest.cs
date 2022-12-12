using NUnit.Framework;
using FluentAssertions;

namespace TagsCloudVisualization.TagsCloudVisualizationTests
{
    [TestFixture]
    internal class FontSettingsTest
    {
        [Test]
        public void Constructor_ShouldNotThrow_OnCorrectInputData()
        {
            var act = () => new FontSettings.FontSettings("Times New Roman", "black");
            act.Should().NotThrow();
        }

        [Test]
        public void Constructor_ShouldSetDefaultFontFamily_OnIncorrectInputFontFamily()
        {
            var res = new FontSettings.FontSettings("Gav-gav font", "black");
            res.FontFamily.Name.Should().Be("Arial");
        }
    }
}
