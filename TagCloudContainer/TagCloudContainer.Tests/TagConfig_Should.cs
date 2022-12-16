using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagConfig_Should 
{
    private TagConfig _tagConfig;

    [Test]
    public void Constructor_ValidArguments_ShouldNotThrowException()
    {
        MainFormConfig.Center = new Point(10, 10);
        MainFormConfig.StandartSize = new Size(10, 10);
        
        var action = () => new TagConfig();
        action
            .Should()
            .NotThrow<ArgumentException>();
    }

    [TestCase(0, 0)]
    [TestCase(-1, -1)]
    public void Constructor_InvalidPoint_ShouldThrowException(int x, int y)
    {
        var invalidPoint = new Point(x, y);
        if (x == 0 && y == 0)
            invalidPoint = new Point();

        MainFormConfig.Center = invalidPoint;
        var action = () => new TagConfig();
        ShouldThrowException(action);
    }
   
    [TestCase(0, 0)]
    [TestCase(-1, -1)]
    public void Constructor_InvalidSize_ShouldThrowException(int width, int height)
    {
        var invalidSize = new Size(width, height);
        if (width == 0 && height == 0)
            invalidSize = new Size();

        MainFormConfig.StandartSize = invalidSize;
        var action = () => new TagConfig();
        ShouldThrowException(action);
    }

    private void ShouldThrowException<T>(Func<T> action)
    {
        action
            .Should()
            .Throw<ArgumentException>();
    }
}