using System.Collections.Generic;
using TagCloud.Core.Util;

namespace TagCloud.Core.WordsParsing.WordsProcessing
{
    public interface IWordsProcessor
    {
        IEnumerable<TagStat> Process(IEnumerable<string> words, HashSet<string> boringWords = null, int? maxUniqueWordsCount = null);
    }
}