using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.App
{
    public interface IWordsFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}
