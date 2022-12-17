using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagConfig_Should 
{
    private TagConfig _tagConfig;
    private IMainFormConfig _mainFormConfig;
    
    [SetUp]
    public void SetUp()
    {
        _mainFormConfig = new MainFormConfig();
    }

    [Test]
    public void Constructor_ValidArguments_ShouldNotThrowException()
    {
        _mainFormConfig.Center = new Point(10, 10);
        _mainFormConfig.StandartSize = new Size(10, 10);
        
        var action = () => new TagConfig(_mainFormConfig);
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

        _mainFormConfig.Center = invalidPoint;
        var action = () => new TagConfig(_mainFormConfig);
        ShouldThrowException(action);
    }
   
    [TestCase(0, 0)]
    [TestCase(-1, -1)]
    public void Constructor_InvalidSize_ShouldThrowException(int width, int height)
    {
        var invalidSize = new Size(width, height);
        if (width == 0 && height == 0)
            invalidSize = new Size();

        _mainFormConfig.StandartSize = invalidSize;
        var action = () => new TagConfig(_mainFormConfig);
        ShouldThrowException(action);
    }

    private void ShouldThrowException<T>(Func<T> action)
    {
        action
            .Should()
            .Throw<ArgumentException>();
    }
}