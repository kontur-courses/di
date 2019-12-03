using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface ITextSpliter
    {
        IEnumerable<string> SplitText(string text);
    }
}
