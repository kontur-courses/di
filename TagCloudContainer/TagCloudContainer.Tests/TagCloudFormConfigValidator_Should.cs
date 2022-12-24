using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Configs;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Forms.Validators;

namespace TagCloudContainer.Tests;

[TestFixture]
public class TagCloudFormConfigValidator_Should
{
    [Test]
    public void ValidateTagCloudFormConfig_CorrectParameters_ShouldBeOk()
    {
        var validator = new TagCloudFormConfigValidator();
        var tagCloudFormConfig = new TagCloudContainerConfig();

        validator
            .Validate(tagCloudFormConfig)
            .IsSuccess
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void ValidateTagCloudFormConfig_TryValidateNull_ShouldBeFail()
    {
        var validator = new TagCloudFormConfigValidator();

        validator
            .Validate(null)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudFormConfig>("Tag cloud form config is null"));
    }
    
    [Test]
    public void ValidateTagCloudFormConfig_EmptyFormSize_ShouldBeFail()
    {
        var validator = new TagCloudFormConfigValidator();
        var tagCloudFormConfig = new TagCloudContainerConfig() { ImageSize = Size.Empty };

        validator
            .Validate(tagCloudFormConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudFormConfig>("Form size can't be empty or null"));
    }
    
    [Test]
    public void ValidateTagCloudFormConfig_InvalidFontFamily_ShouldBeFail()
    {
        var validator = new TagCloudFormConfigValidator();
        var tagCloudFormConfig = new TagCloudContainerConfig() { FontFamily = Guid.NewGuid().ToString("N") };

        validator
            .Validate(tagCloudFormConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudFormConfig>("Incorrect font family"));
    }
}