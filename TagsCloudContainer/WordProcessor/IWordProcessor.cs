using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer.WordProcessor
{
    public interface IWordProcessor
    {
        IEnumerable<WordWithCount> ProcessWords(IEnumerable<string> words);
    }
}
