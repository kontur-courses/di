using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer.Reader
{
    public interface IFileReader
    {
        IList<string> ReadWords(string filename);
    }
}
