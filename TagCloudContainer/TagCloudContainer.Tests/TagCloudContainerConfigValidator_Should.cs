using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Configs;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Forms.Validators;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagCloudContainerConfigValidator_Should
{
    [Test]
    public void ValidateContainerConfig_CorrectParameters_ShouldBeOk()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();

        validator
            .Validate(tagCloudContainerConfig)
            .IsSuccess
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void ValidateContainerConfig_IncorrectMainDirectoryPath_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig()
        {
            MainDirectoryPath = Path.Combine(TagCloudContainerConfig.GetMainDirectoryPath(), Guid.NewGuid().ToString("N"))
        };
        
        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Incorrect path to main directory"));
    }
    
    [Test]
    public void ValidateContainerConfig_Null_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        
        validator
            .Validate(null)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Tag cloud config is null"));
    }
    
    [Test]
    public void ValidateContainerConfig_EmptyCenterPoint_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig() { Center = Point.Empty };
        
        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Center point can't be empty"));
    }
    
    [Test]
    public void ValidateContainerConfig_EmptyStandartSize_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig() { StandartSize = Size.Empty };

        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Standart size can't be empty"));
    }
    
    [Test]
    public void ValidateWordReaderConfig_IncorrectFilePath_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();
        tagCloudContainerConfig.SetFilePath("test.cs");

        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Words file does not exist"));
    }
    
    [Test]
    public void ValidateWordReaderConfig_IncorrectExcludingWordsFilePath_ShouldBeFail()
    {
        var validator = new TagCloudContainerConfigValidator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();
        tagCloudContainerConfig.SetExcludeWordsFilePath("");

        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Exclude words file does not exist"));
    }
    
}