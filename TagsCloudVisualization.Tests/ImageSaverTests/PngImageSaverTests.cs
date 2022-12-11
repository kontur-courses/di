using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.ImageSavers;

namespace TagsCloudVisualization.Tests.ImageSaverTests;

[TestFixture]
public class PngImageSaverTests
{
    private string filename = "test";
    private string filepath;

    public PngImageSaverTests()
    {
        filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);
    }

    [TearDown]
    public void DeleteFile()
    {
        File.Delete($"{filepath}.png");
    }


    [Test]
    public void Save_ShouldSaveImage()
    {
        var imageSaver = new PngImageSaver();

        imageSaver.Save(filepath, new Bitmap(10, 10));

        var files = Directory.GetFiles(Directory.GetCurrentDirectory()).Select(Path.GetFileName);
        files.Should().Contain($"{filename}.png");
    }
}