using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.WordPrework
{
    interface IWordAnalyzer
    {
        Dictionary<string, int> GetWordFrequency();

        Dictionary<string, int> GetSpecificWordFrequency(params PartOfSpeech[] partsOfSpeech);
    }
}
