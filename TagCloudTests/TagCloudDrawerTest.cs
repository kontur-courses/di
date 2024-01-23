using System.Drawing;
using FluentAssertions;
using TagCloud;

namespace TagCloudTests;

public class TagCloudDrawerTest
{
    private const string RelativePathToTestDirectory = @"..\..\..\Test";

    private static readonly string Path = System.IO.Path.GetFullPath(RelativePathToTestDirectory);
    private readonly DirectoryInfo directory = new DirectoryInfo(Path);

    private TagCloudDrawer drawer;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        foreach (var file in directory.EnumerateFiles())
            file.Delete();
    }

    [SetUp]
    public void SetUp()
    {
        drawer = TagCloudDrawer.Create(
            Path, 
            TestContext.CurrentContext.Test.Name, 
            1,
            new ConstantColorSelector(Color.Black)
        );
    }

    [Test]
    public void Throw_ThenDrawEmptyList()
    {
        Assert.Throws<ArgumentException>(() => drawer.Draw(Array.Empty<TextRectangle>()));
    }

    [Test]
    public void Throw_ThenDirectoryDoesNotExist()
    {
        Assert.Throws<ArgumentException>(() => TagCloudDrawer.Create(
            "PathDontExist",
            "xxx",
            1,
            new ConstantColorSelector(Color.Black))
        );
    }

    [Test]
    public void DrawOneCase()
    {
        var testRect = new TextRectangle(
            new Rectangle(0, 0, 1, 1),
            "abc",
            new Font(FontFamily.GenericSerif, 10)
        );
        var textRectangles = Enumerable.Repeat(testRect, 3);
        drawer.Draw(textRectangles);
        directory
            .EnumerateFiles()
            .Count(file => file.Name.Contains(nameof(DrawOneCase)))
            .Should()
            .Be(1);
    }
}