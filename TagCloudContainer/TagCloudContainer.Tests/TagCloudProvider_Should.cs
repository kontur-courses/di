using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagCloudProvider_Should
{
    private IMainFormConfig _mainFormConfig;
    
    [Test]
    public void Constructor_InvalidFileName_ShouldThrowArgumentException()
    {
        var action = () => new TagCloudProvider("", new Point(), new Size(), _mainFormConfig);

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void Constructor_InvalidCenter_ShouldThrowArgumentException()
    {
        var invalidPoint = Point.Empty;
        var action = () => new TagCloudProvider("file.txt", invalidPoint, new Size(), _mainFormConfig);

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void Constructor_InvalidSize_ShouldThrowArgumentException()
    {
        var invalidSize = Size.Empty;
        var action = () => new TagCloudProvider("file.txt", new Point(10, 10), invalidSize, _mainFormConfig);

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
}