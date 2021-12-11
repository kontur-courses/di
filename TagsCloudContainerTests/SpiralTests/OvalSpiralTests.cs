using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class OvalSpiralTests : SpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            var settings = SettingsProvider.GetSettings();
            center = settings.Center;
            spiral = new OvalSpiral(settings);
        }
    }
}
