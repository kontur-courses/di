using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class PrimaryPainterTests : PainterTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var settings = SettingsProvider.GetSettings();
            painter = new PrimaryTagPainter(settings);
            selector = tag => tag.Color.A;
            expected = new[] { "First", "Second", "Third" };
        }
    }
}
