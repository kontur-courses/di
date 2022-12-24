using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Forms.Interfaces;

namespace TagCloudContainer.Forms.Validators;

public class TagCloudFormConfigValidator : IConfigValidator<ITagCloudFormConfig>
{
    public Result<ITagCloudFormConfig> Validate(ITagCloudFormConfig tagCloudFormConfig)
    {
        if (tagCloudFormConfig.ImageSize.IsEmpty) 
            return Result.Fail<ITagCloudFormConfig>("Form size can't be empty");
        
        return Result.Ok(tagCloudFormConfig);
    }    
}