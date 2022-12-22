using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Additions.Models;

public class TagCloudFormResult : ITagCloudFormResult
{
    public Result<ITagCloudFormConfig> FormResult { get; set; } = new Result<ITagCloudFormConfig>();
}