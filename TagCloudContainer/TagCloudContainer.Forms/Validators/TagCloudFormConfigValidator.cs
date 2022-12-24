using System.Drawing.Text;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Forms.Interfaces;

namespace TagCloudContainer.Forms.Validators;

public class TagCloudFormConfigValidator : IConfigValidator<ITagCloudFormConfig>
{
    public Result<ITagCloudFormConfig> Validate(ITagCloudFormConfig tagCloudFormConfig)
    {
        var systemFonts = new InstalledFontCollection().Families.Select(f => f.Name);
        
        if (tagCloudFormConfig == null)
            return Result.Fail<ITagCloudFormConfig>("Tag cloud form config is null");
        if (tagCloudFormConfig.ImageSize.IsEmpty || tagCloudFormConfig.ImageSize == null) 
            return Result.Fail<ITagCloudFormConfig>("Form size can't be empty or null");
        if (tagCloudFormConfig.Color == null) 
            return Result.Fail<ITagCloudFormConfig>("Color can't be null");
        if (tagCloudFormConfig.BackgroundColor == null) 
            return Result.Fail<ITagCloudFormConfig>("Background color can't be null");
        if (!systemFonts.Contains(tagCloudFormConfig.FontFamily)) 
            return Result.Fail<ITagCloudFormConfig>("Incorrect font family");
        
        return Result.Ok(tagCloudFormConfig);
    }    
}