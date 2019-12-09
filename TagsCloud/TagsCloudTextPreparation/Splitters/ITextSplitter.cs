using System.Collections.Generic;

namespace TagsCloudTextPreparation.Splitters
{
    public interface ITextSplitter
    {
        IEnumerable<string> SplitText(string text);
    }
}