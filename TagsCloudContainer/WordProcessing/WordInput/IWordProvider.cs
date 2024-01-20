using TagsCloudContainer.Common;

namespace TagsCloudContainer.WordProcessing.WordInput;

public interface IWordProvider
{
    public string[] Words { get; }
}