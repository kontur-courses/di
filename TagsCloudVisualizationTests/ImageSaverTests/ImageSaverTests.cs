using FluentAssertions;
using NUnit.Framework;
using System.Drawing.Imaging;
using TagsCloudVisualization.ImageSavers;

namespace TagsCloudVisualizationTests.ImageSaverTests;

[TestFixture]
public class ImageSaverTests
{
    [TestCase("file", TestName = "name without special symbols")]
    [TestCase("file_name", TestName = "name with underscore")]
    [TestCase("(file)", TestName = "name with brackets")]
    [TestCase("file-", TestName = "name with dash")]
    [TestCase("file.", TestName = "name with dot")]
    [TestCase("file,", TestName = "name with comma")]
    public void IsFileNameValid_ValidFileName_ReturnTrue(string fileName)
    {
        var result = ImageSaver.IsFileNameValid(fileName);
        result.Should().BeTrue();
    }

    [TestCase("\"file", TestName = "name with quotes")]
    [TestCase("\0file", TestName = "name with null char")]
    [TestCase("?file", TestName = "name with question mark")]
    [TestCase("*file", TestName = "name with star")]
    public void IsFileNameValid_InvalidFileNames_ReturnFalse(string fileName)
    {
        var result = ImageSaver.IsFileNameValid(fileName);
        result.Should().BeFalse();
    }

    [TestCase("/path", TestName = "name with slash")]
    [TestCase("path", TestName = "name without special symbols")]
    public void IsPathValid_ValidPathName_ReturnTrue(string pathName)
    {
        var result = ImageSaver.IsPathValid(pathName);
        result.Should().BeTrue();
    }

    [TestCase("\0path", TestName = "name with null char")]
    [TestCase("|path", TestName = "name with vertical bar")]
    public void IsPathValid_InvalidPathName_ReturnFalse(string pathName)
    {
        var result = ImageSaver.IsPathValid(pathName);
        result.Should().BeFalse();
    }

    [TestCaseSource(typeof(ImageSaverTestData), nameof(ImageSaverTestData.TestData))]
    public void GetImageFormat_DifferentFormats_ReturnsCorrectFormat(string format, ImageFormat excepted)
    {
        var result = ImageSaver.GetImageFormat(format);
        result.Should().Be(excepted);
    }
}
