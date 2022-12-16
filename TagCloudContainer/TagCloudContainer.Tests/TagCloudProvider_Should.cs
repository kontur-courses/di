using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagCloudProvider_Should
{
    [Test]
    public void Constructor_InvalidCenter_ShouldThrowArgumentException()
    {
        MainFormConfig.Center = Point.Empty;
        var action = () => new TagCloudProvider(new TagConfig(), new WordsReader(new WordConfig()));

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void Constructor_InvalidSize_ShouldThrowArgumentException()
    {
        MainFormConfig.StandartSize = Size.Empty;
        var action = () => new TagCloudProvider(new TagConfig(), new WordsReader(new WordConfig()));

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
}