using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer.Reader
{
    public interface ITextReader
    {
        IEnumerable<string> ReadWords(string path);
    }
}
