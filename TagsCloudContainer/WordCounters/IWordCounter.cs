using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordCounters
{
    interface IWordCounter
    {
        List<WordToken> CountWords(List<string> words);
    }
}
