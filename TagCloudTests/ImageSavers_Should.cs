using System.Drawing;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;
using TagCloudCreatorExtensions.ImageSavers;

namespace TagCloudTests;

[TestFixture]
public class ImageSavers_Should
{
    private IImagePathSettingsProvider _pathSettingsProvider = null!;
    private Image _image = null!;

    [SetUp]
    public void Setup()
    {
        var pathSettings = A.Fake<IImagePathSettings>();
        _pathSettingsProvider = A.Fake<IImagePathSettingsProvider>();
        A.CallTo(() => _pathSettingsProvider.GetImagePathSettings())
            .Returns(pathSettings);
        _image = new Bitmap(800, 600);
    }

    [TearDown]
    public void TearDown()
    {
        _image.Dispose();
    }

    [TestCaseSource(nameof(ImagesFormatsTestCaseData))]
    public void SaveImage_Successfully(Func<IImagePathSettingsProvider, IImageSaver> saverProvider)
    {
        var saver = saverProvider(_pathSettingsProvider);
        var imageName = $"image{saver.SupportedExtension}";
        RemoveIfExists(imageName);
        _pathSettingsProvider.GetImagePathSettings().ImagePath = imageName;
        saver.SaveImage(_image);

        File.Exists(imageName).Should().BeTrue();
        RemoveIfExists(imageName);
    }

    private static TestCaseData[] ImagesFormatsTestCaseData =
    {
        new TestCaseData
        (
            new Func<IImagePathSettingsProvider, IImageSaver>(provider => new PngImageSaver(provider))
        ).SetName("Png")
    };

    private static void RemoveIfExists(string file)
    {
        if (File.Exists(file))
            File.Delete(file);
    }
}