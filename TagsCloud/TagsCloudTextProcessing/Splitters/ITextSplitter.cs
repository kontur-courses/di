using System.Collections.Generic;

namespace TagsCloudTextProcessing.Splitters
{
    public interface ITextSplitter
    {
        IEnumerable<string> SplitText(string text);
    }
}