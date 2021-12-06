using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class NoneFilter : IWordFilter
{
    public bool IsValid(string word)
    {
        return true;
    }
}
