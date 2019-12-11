using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using TagsCloudVisualization;
using System.Drawing.Imaging;
using System;

namespace TagsCloudApplicationTests
{
    [TestFixture]
    public class ImageUtilities_Should
    {
        private Dictionary<string, ImageFormat> formatNameToFormat;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            formatNameToFormat = new Dictionary<string, ImageFormat>()
            {
                { "png" , ImageFormat.Png },
                { "jpeg", ImageFormat.Jpeg },
                { "gif", ImageFormat.Gif },
                { "bmp", ImageFormat.Bmp },
                { "tiff", ImageFormat.Tiff }
            };
        }

        [TestCase("png")]
        [TestCase("jpeg")]
        [TestCase("gif")]
        public void ParseFormatNameString_Properly(string formatNameString)
        {
            ImageUtilities.GetFormatFromString(formatNameString).Should().BeEquivalentTo(
                formatNameToFormat[formatNameString]);
        }

        [TestCase("PNG")]
        [TestCase("PnG")]
        public void ParseFormatNameString_IndifferentToCase(string formatNameString)
        {
            ImageUtilities.GetFormatFromString(formatNameString).Should().BeEquivalentTo(
                formatNameToFormat[formatNameString.ToLower()]);
        }

        [Test]
        public void ThrowArgumentException_OnUnknownFormatName()
        {
            Action act = () => ImageUtilities.GetFormatFromString("???");

            act.Should().Throw<ArgumentException>();
        }
    }
}
