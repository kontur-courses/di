using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Additions;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Tests;

[TestFixture]
public class Validator_Should
{
    [Test]
    public void ValidateContainerConfig_CorrectParameters_ShouldBeOk()
    {
        var validator = new Validator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();

        validator
            .Validate(tagCloudContainerConfig)
            .IsSuccess
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void ValidateContainerConfig_EmptyCenterPoint_ShouldBeFail()
    {
        var validator = new Validator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();
        
        tagCloudContainerConfig.Center = Point.Empty;
        
        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Center point can't be empty"));
    }
    
    [Test]
    public void ValidateContainerConfig_EmptyStandartSize_ShouldBeFail()
    {
        var validator = new Validator();
        var tagCloudContainerConfig = new TagCloudContainerConfig();
        
        tagCloudContainerConfig.StandartSize = Size.Empty;

        validator
            .Validate(tagCloudContainerConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudContainerConfig>("Standart font size can't be empty"));
    }
    
    [Test]
    public void ValidateWordReaderConfig_CorrectParameters_ShouldBeOk()
    {
        var validator = new Validator();
        var wordReaderConfig = new WordReaderConfig();

        validator
            .Validate(wordReaderConfig)
            .IsSuccess
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void ValidateWordReaderConfig_IncorrectFilePath_ShouldBeFail()
    {
        var validator = new Validator();
        var wordReaderConfig = new WordReaderConfig();
        wordReaderConfig.SetFilePath("test.cs");

        validator
            .Validate(wordReaderConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<IWordReaderConfig>("Words file does not exist"));
    }
    
    [Test]
    public void ValidateWordReaderConfig_IncorrectExcludingWordsFilePath_ShouldBeFail()
    {
        var validator = new Validator();
        var wordReaderConfig = new WordReaderConfig();
        wordReaderConfig.SetExcludeWordsFilePath("");

        validator
            .Validate(wordReaderConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<IWordReaderConfig>("Exclude words file does not exist"));
    }
    
    [Test]
    public void ValidateTagCloudFormConfig_CorrectParameters_ShouldBeOk()
    {
        var validator = new Validator();
        var tagCloudFormConfig = new TagCloudFormConfig();

        validator
            .Validate(tagCloudFormConfig)
            .IsSuccess
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void ValidateTagCloudFormConfig_EmptyFormSize_ShouldBeFail()
    {
        var validator = new Validator();
        var tagCloudFormConfig = new TagCloudFormConfig();
        tagCloudFormConfig.FormSize = Size.Empty;

        validator
            .Validate(tagCloudFormConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudFormConfig>("Form size can't be empty"));
    }
}