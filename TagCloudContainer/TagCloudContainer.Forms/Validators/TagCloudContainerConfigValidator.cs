using TagCloudContainer.Configs;
using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Forms.Interfaces;

namespace TagCloudContainer.Forms.Validators;

public class TagCloudContainerConfigValidator : IConfigValidator<ITagCloudContainerConfig>
{
    public Result<ITagCloudContainerConfig> Validate(ITagCloudContainerConfig tagCloudContainerConfig)
    {
        if (tagCloudContainerConfig == null)
            return Result.Fail<ITagCloudContainerConfig>("Tag cloud config is null");
        if (!Directory.Exists(tagCloudContainerConfig.MainDirectoryPath))
            return Result.Fail<ITagCloudContainerConfig>("Incorrect path to main directory");
        if (tagCloudContainerConfig.Center.IsEmpty)
            return Result.Fail<ITagCloudContainerConfig>("Center point can't be empty");
        if (tagCloudContainerConfig.StandartSize.IsEmpty)
            return Result.Fail<ITagCloudContainerConfig>("Standart size can't be empty");
        if (!File.Exists(tagCloudContainerConfig.FilePath))
            return Result.Fail<ITagCloudContainerConfig>("Words file does not exist");
        if (!File.Exists(tagCloudContainerConfig.ExcludeWordsFilePath))
            return Result.Fail<ITagCloudContainerConfig>("Exclude words file does not exist");
        
        return Result.Ok(tagCloudContainerConfig);       
    }
}