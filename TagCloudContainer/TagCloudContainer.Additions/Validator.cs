using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Additions;

public class Validator
{
    public Result<ITagCloudContainerConfig> Validate(ITagCloudContainerConfig tagCloudContainerConfig)
    {
        if (tagCloudContainerConfig.Center.IsEmpty)
            return Result.Fail<ITagCloudContainerConfig>("Center point can't be empty");
        if (tagCloudContainerConfig.StandartSize.IsEmpty)
            return Result.Fail<ITagCloudContainerConfig>("Standart font size can't be empty");
        
        return Result.Ok(tagCloudContainerConfig);
    }
    
    public Result<IWordReaderConfig> Validate(IWordReaderConfig wordReaderConfig)
    {
        if (!File.Exists(wordReaderConfig.FilePath))
            return Result.Fail<IWordReaderConfig>("Words file does not exist");
        if (!File.Exists(wordReaderConfig.ExcludeWordsFilePath))
            return Result.Fail<IWordReaderConfig>("Exclude words file does not exist");
        
        return Result.Ok(wordReaderConfig);
    }

    public Result<ITagCloudFormConfig> Validate(ITagCloudFormConfig tagCloudFormConfig)
    {
        if (tagCloudFormConfig.FormSize.IsEmpty) 
            return Result.Fail<ITagCloudFormConfig>("Form size can't be empty");
        
        return Result.Ok(tagCloudFormConfig);
    }
}