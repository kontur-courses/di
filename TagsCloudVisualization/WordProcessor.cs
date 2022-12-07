using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    internal static class WordProcessor
    {
        static private bool WordIsAllowed(string word, ProcessingSettings settings)
        {
            return word.Length >= settings.MinWordLength &&
                   word.Length <= settings.MaxWordLength &&
                   !settings.ExcludedWords.Contains(word);
        }

        static public string[] Process(string[] words, ProcessingSettings settings)
        {
            return words.Where(word => WordIsAllowed(word, settings))
                 .Select(word => word.ToLower()).ToArray();
        }
    }
}
