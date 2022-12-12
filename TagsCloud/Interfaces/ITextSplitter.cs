using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface ITextSplitter
    {
        public IEnumerable<string> SplitTextOnWords(string text);
    }
}
