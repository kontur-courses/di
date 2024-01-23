using NUnit.Framework.Internal;
using TagCloud.FileSaver;

namespace TagCloudTests;

[TestFixture]
public class CloudSaver_Should
{
    private ISaver sut = new ImageSaver();
    private const string filename = "test";
    private const string extension = "png";


    [Test]
    public void SaveBitmapToFile()
    {
        using var bitmap = new Bitmap(50, 50);

        sut.Save(bitmap, filename, extension);

        File.Exists($"{filename}.{extension}").Should().BeTrue();

        File.Delete($"{filename}.{extension}");
    }
}