using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> ToWords(this IEnumerable<WordInfo> wordInfos) =>
            wordInfos.Select(wordInfo => wordInfo.Word);
    }
}