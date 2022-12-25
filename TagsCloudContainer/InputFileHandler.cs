using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    /// <summary>
    /// Обрабатывает входную последовательность слов. На выходе - частотный словарь без скучных слов.
    /// </summary>
    public static class InputFileHandler
    {
        public static Dictionary<string, int> FormFrequencyDictionary(IEnumerable<string> words, IUi settings)
        {
            var filteredWords = BoringWordsDeleter.DeleteBoringWords(words, settings);
            return filteredWords
                .Select(word => word.ToLower())
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}