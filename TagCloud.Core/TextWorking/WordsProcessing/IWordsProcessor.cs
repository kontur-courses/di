using System.Collections.Generic;
using TagCloud.Util;

namespace TagCloud.Core.TextWorking.WordsProcessing
{
    public interface IWordsProcessor
    {
        IEnumerable<TagStat> Process(IEnumerable<string> words, HashSet<string> boringWords = null, int? maxUniqueWordsCount = null);
    }
}