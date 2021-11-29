using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> ToWords(this IEnumerable<WordInfo> wordInfos) =>
            wordInfos.Select(wordInfo => wordInfo.Word);
    }
}