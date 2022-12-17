using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    public class InputFileHandler : IInputTextProvider
    {
        public IWordFilter WordsFilter { get; set; }

        public InputFileHandler(IWordFilter wordFilter)
        {
            WordsFilter = wordFilter;
        }
        public Dictionary<string, int> GetWords(string text)
        {
            text = text.ToLower();
            Dictionary<string, int> words = new Dictionary<string, int>();
            foreach (var word in text.Split(' ', '\n', '\r'))
            {
                if (WordsFilter.WordsToFilter.Contains(word) || word.Equals("")) continue;
                if (!words.Keys.Contains(word)) words[word] = 0;
                words[word]++;
            }

            return words;
        }
    }
}