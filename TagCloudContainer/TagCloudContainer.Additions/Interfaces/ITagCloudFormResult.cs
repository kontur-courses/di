namespace TagCloudContainer.Additions.Interfaces;

public interface ITagCloudFormResult
{
    public Result<ITagCloudFormConfig> FormResult { get; set; }
}