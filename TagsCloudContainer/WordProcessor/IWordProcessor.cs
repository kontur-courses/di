using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer.WordProcessor
{
    public interface IWordProcessor
    {
        IDictionary<string, int> ProcessWords(IList<string> words);
    }
}
