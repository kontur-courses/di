using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    interface IWordCounter
    {
        List<Tuple<string, int>> CountWords(List<string> words);
    }
}
