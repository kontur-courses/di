using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
    public interface ITextSpliter
    {
        IEnumerable<string> SplitText(string text);
    }
}
