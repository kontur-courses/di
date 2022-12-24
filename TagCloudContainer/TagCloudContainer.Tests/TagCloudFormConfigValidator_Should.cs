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
    public void ValidateTagCloudFormConfig_EmptyFormSize_ShouldBeFail()
    {
        var validator = new TagCloudFormConfigValidator();
        var tagCloudFormConfig = new TagCloudContainerConfig();
        tagCloudFormConfig.ImageSize = Size.Empty;

        validator
            .Validate(tagCloudFormConfig)
            .Should()
            .BeEquivalentTo(Result.Fail<ITagCloudFormConfig>("Form size can't be empty"));
    }
}