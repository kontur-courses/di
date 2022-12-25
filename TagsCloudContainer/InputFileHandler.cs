using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    /// <summary>
    /// Обрабатывает входной файл. На выходе - частотный словарь без скучных слов.
    /// </summary>
    public static class InputFileHandler
    {
        public static Dictionary<string, int> FormFrequencyDictionary(IEnumerable<string> words, IUi settings)
        {
            words = words.Select(word => word.ToLower()).ToArray();
            
            var filteredWords = BoringWordsDeleter.DeleteBoringWords(words, settings);
            return filteredWords.GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}