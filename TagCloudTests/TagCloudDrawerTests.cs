using System.Drawing;
using TagCloud;

namespace TagCloudTests;

public class TagCloudDrawerTests
{
    private const string RelativePathToTestDirectory = @"..\..\..\Test";

    private string path;
    private DirectoryInfo directory;

    private TagCloudDrawer drawer;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        if (!Path.Exists(RelativePathToTestDirectory))
            Directory.CreateDirectory(RelativePathToTestDirectory);
        
        path = Path.GetFullPath(RelativePathToTestDirectory);
        directory = new DirectoryInfo(path);
        
        foreach (var file in directory.EnumerateFiles())
            file.Delete();
    }

    [SetUp]
    public void SetUp()
    {
        drawer = TagCloudDrawer.Create(
            path, 
            TestContext.CurrentContext.Test.Name, 
            new Font(FontFamily.GenericSerif, 1),
            new ConstantColorSelector(Color.Black)
        );
    }

    [Test]
    public void Throw_ThenDrawEmptyList()
    {
        Assert.Throws<ArgumentException>(() => drawer.Draw(new List<TextRectangle>()));
    }

    [Test]
    public void Throw_ThenDirectoryDoesNotExist()
    {
        Assert.Throws<ArgumentException>(() => TagCloudDrawer.Create(
            path: "PathDontExist",
            name: "xxx",
            new Font(FontFamily.GenericSerif, 1),
            new ConstantColorSelector(Color.Black))
        );
    }

    [Test]
    public void DrawOneTundredRectangles()
    {
        var testRect = new TextRectangle(
            new Rectangle(0, 0, 1, 1),
            text: "abc",
            new Font(FontFamily.GenericSerif, 10)
        );
        var textRectangles = Enumerable.Repeat(testRect, 100).ToList();
        drawer.Draw(textRectangles);
        directory
            .EnumerateFiles()
            .Count(file => file.Name.Contains(nameof(DrawOneTundredRectangles)))
            .Should()
            .Be(1);
    }
}