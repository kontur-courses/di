using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudCore.Drawing;

namespace TagsCloudCoreTests.Drawing;

public class DefaultImageDrawerTests
{
    [TestCase("test", "<>\\")]
    [TestCase("test", "name|123")]
    [TestCase("test", "")]
    [TestCase("", "filename")]
    [TestCase("  ", "filename")]
    [TestCase(@"\:\", "filename")]
    public void SaveImage_ThrowsArgumentException_OnInvalidParameters(string dirPath, string filename)
    {
        Assert.Throws<ArgumentException>(() =>
            DefaultImageDrawer.SaveImage(new Bitmap(1, 1), dirPath, filename, ImageFormat.Png));
    }
}