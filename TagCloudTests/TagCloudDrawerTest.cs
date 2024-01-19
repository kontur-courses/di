using System.Drawing;
using FluentAssertions;
using TagCloud;

namespace TagCloudTests;

public class TagCloudDrawerTest
{
    private const string RelativePathToTestDirectory = @"..\..\..\Test";
    
    private static readonly string path = Path.GetFullPath(RelativePathToTestDirectory);
    private readonly DirectoryInfo directory = new DirectoryInfo(path);
    
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
        drawer = TagCloudDrawer.Create(path, new ConstantColorSelector(Color.Black));
    }

    [Test]
    public void Throw_ThenDrawEmptyList()
    {
        Assert.Throws<ArgumentException>(() => drawer.Draw(Array.Empty<Rectangle>(), "throw"));
    }
    
    [Test]
    public void Throw_ThenDirectoryDoesNotExist()
    {
        Assert.Throws<ArgumentException>(() => TagCloudDrawer.Create(
            $@"{path}\DontExist", 
            new ConstantColorSelector(Color.Black))
        );
    }
    
    [Test]
    public void DrawOneCase()
    {
        var rectangles = new Rectangle[]
        {
            new Rectangle(0, 1, 2, 3),
            new Rectangle(4, 5, 6, 7),
            new Rectangle(8, 9, 10, 11),
        };
        drawer.Draw(rectangles, "three_rectangles");
        directory
            .EnumerateFiles()
            .Count(file => file.Name.Contains("three_rectangles"))
            .Should()
            .Be(1);
    }
}