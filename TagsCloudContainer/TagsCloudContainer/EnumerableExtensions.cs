using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudContainer
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> ToStrings(this IEnumerable<WordInfo> wordInfos) =>
            wordInfos.Select(wordInfo => wordInfo.Word);
    }
}