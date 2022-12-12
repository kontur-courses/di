using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public static class BoringWordsDeleter
    {
        public static string[] DeleteBoringWords(IEnumerable<string> words)
        {
            return words.Where(word => word.Length > 3).ToArray();
        }
    }
}