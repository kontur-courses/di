using System.Drawing;
using FluentAssertions;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagCloudProvider_Should
{
    private IMainFormConfig _mainFormConfig;
    
    [SetUp]
    public void SetUp()
    {
        _mainFormConfig = new MainFormConfig();
    }
    
    [Test]
    public void Constructor_CorrectParameters_ShouldNotThrowArgumentException()
    {
        var action = () => new TagCloudProvider(
            new TagConfig(_mainFormConfig), 
            new WordsReader(new WordConfig(_mainFormConfig), _mainFormConfig),
            _mainFormConfig
            );

        action
            .Should()
            .NotThrow();
    }
    
    [Test]
    public void Constructor_InvalidCenter_ShouldThrowArgumentException()
    {
        _mainFormConfig.Center = Point.Empty;
        var action = () => new TagCloudProvider(
            new TagConfig(_mainFormConfig), 
            new WordsReader(new WordConfig(_mainFormConfig), _mainFormConfig),
            _mainFormConfig
            );

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Test]
    public void Constructor_InvalidSize_ShouldThrowArgumentException()
    {
        _mainFormConfig.StandartSize = Size.Empty;
        var action = () => new TagCloudProvider(
            new TagConfig(_mainFormConfig), 
            new WordsReader(new WordConfig(_mainFormConfig), _mainFormConfig),
            _mainFormConfig
            );

        action
            .Should()
            .Throw<ArgumentException>();
    }
    
}