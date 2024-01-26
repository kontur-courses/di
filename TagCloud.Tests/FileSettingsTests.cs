using System.Reflection;
using TagCloud.Domain.Settings;

namespace TagCloud.Tests;

public class FileSettingsTests
{
    [SetUp]
    public void SetUp()
    {
        settings = new FileSettings();
    }

    private FileSettings settings;

    [Test]
    public void OutFileName_ThrowsArgumentExceptionOnIncorrectFIleName()
    {
        new Action(() =>
            {
                settings.OutFileName = "/a.png";
            })
            .Should()
            .ThrowExactly<ArgumentException>()
            .Where(e => e.Message.Equals("Name /a.png is invalid", StringComparison.OrdinalIgnoreCase));
    }
    
    [Test]
    public void OutFileName_ThrowsArgumentExceptionOnIncorrectPath()
    {
        new Action(() =>
            {
                settings.OutPathToFile = "\0/dir";
            })
            .Should()
            .ThrowExactly<ArgumentException>()
            .Where(e => e.Message.Equals("Path \0/dir is invalid", StringComparison.OrdinalIgnoreCase));
    }
    
    [Test]
    public void FileFromWithPath_ThrowsArgumentExceptionWhenFileNotFound()
    {
        new Action(() =>
            {
                settings.FileFromWithPath = "somedir/somefile.ext";
            })
            .Should()
            .ThrowExactly<ArgumentException>()
            .Where(e => e.Message.Equals("There is no file by path somedir/somefile.ext", StringComparison.OrdinalIgnoreCase));
    }
    
    [Test]
    public void FileFromWithPath_ShouldNotThrowOnCorrectFilePath()
    {
        new Action(() =>
            {
                settings.FileFromWithPath = $"../../../{nameof(FileSettingsTests)}.cs";
            })
            .Should()
            .NotThrow();
    }
}