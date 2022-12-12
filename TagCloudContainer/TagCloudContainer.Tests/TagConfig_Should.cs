using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class Tests
{
    private TagConfig _tagConfig;
    private MainFormConfig _mainFormConfig = new MainFormConfig() {};

    [Test]
    public void Constructor_ValidArguments_ShouldNotThrowException()
    {
        var action = () => new TagConfig(new Point(10, 10), new Size(10, 10), _mainFormConfig);
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
        
        var action = () => new TagConfig(invalidPoint, new Size(10, 10), _mainFormConfig);
        ShouldThrowException(action);
    }
   
    [TestCase(0, 0)]
    [TestCase(-1, -1)]
    public void Constructor_InvalidSize_ShouldThrowException(int width, int height)
    {
        var invalidSize = new Size(width, height);
        if (width == 0 && height == 0)
            invalidSize = new Size();
        
        var action = () => new TagConfig(new Point(10, 10), invalidSize, _mainFormConfig);
        ShouldThrowException(action);
    }

    private void ShouldThrowException<T>(Func<T> action)
    {
        action
            .Should()
            .Throw<ArgumentException>();
    }
}