using NUnit.Framework.Internal;
using TagCloud.CloudSaver;

namespace TagCloudTests;

[TestFixture]
public class CloudSaver_Should
{
    private ICloudSaver sut = new CloudSaver();
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