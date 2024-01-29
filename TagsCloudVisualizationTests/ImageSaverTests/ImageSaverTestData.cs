using NUnit.Framework;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationTests.ImageSaverTests;

public static class ImageSaverTestData
{
    public static TestCaseData[] TestData =
    {
        new TestCaseData("png", ImageFormat.Png).SetName("png format"),
        new TestCaseData("jpeg", ImageFormat.Jpeg).SetName("jpeg format"),
        new TestCaseData("tiff", ImageFormat.Tiff).SetName("tiff format"),
        new TestCaseData("emf", ImageFormat.Emf).SetName("emf format"),
        new TestCaseData("wmf", ImageFormat.Wmf).SetName("wmf format"),
        new TestCaseData("gif", ImageFormat.Gif).SetName("gif format"),
        new TestCaseData("exif", ImageFormat.Exif).SetName("exif format")
    };
}
