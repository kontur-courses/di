using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.PointsProviders;

namespace TagsCloudVisualizationTests;

public class BitmapExtensionsTests
{
    private Bitmap bitmap;
    private string correctDirectoryPath;
    private string correctFileName;

    [SetUp]
    public void SetUp()
    {
        bitmap = new Bitmap(100, 100);
        correctDirectoryPath = @"../";
        correctFileName = "asd";
    }
    
    [Test]
    public void SaveAs_ThrowsException_OnIncorrectDirectoryPath()
    {
        foreach (var invalidPathChar in Path.GetInvalidPathChars())
        {
            var directoryPath = correctDirectoryPath + invalidPathChar;
            var a = () => bitmap.SaveAs(directoryPath, correctFileName, ImageFormat.Png);
            a.Should().Throw<ArgumentException>();
        }
    }
    
    [Test]
    public void SaveAs_ThrowsException_OnIncorrectFileName()
    {
        foreach (var invalidNameChar in Path.GetInvalidFileNameChars())
        {
            var directoryPath = correctDirectoryPath + invalidNameChar;
            var a = () => bitmap.SaveAs(correctDirectoryPath, directoryPath, ImageFormat.Png);
            a.Should().Throw<ArgumentException>();
        }
    }
    
    [Test]
    public void SaveAs_ThrowsException_OnNullDirectoryPath()
    {
        var a = () => bitmap.SaveAs(null, correctFileName, ImageFormat.Png);
        
        a.Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void SaveAs_ThrowsException_OnNullFileName()
    {
        var a = () => bitmap.SaveAs(correctDirectoryPath, null, ImageFormat.Png);
        
        a.Should().Throw<ArgumentException>();
    }
    
}