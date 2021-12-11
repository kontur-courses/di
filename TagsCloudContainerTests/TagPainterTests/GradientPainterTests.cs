using NUnit.Framework;
using System;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class GradientPainterTests : PainterTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var settings = SettingsProvider.GetSettings();
            palette = settings.Palette;
            painter = new GradientTagPainter(settings);
            selector = tag => Math.Abs(tag.Color.R - palette.Primary.R);
            expected = new[] { "Third" , "First", "Second" };
        }
    }
}
