using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    public class InputFileHandler : IInputTextProvider
    {
        public string InputFilePath;
        public WordFilter WordsFilter;

        public InputFileHandler(WordFilter wordFilter)
        {
            WordsFilter = wordFilter;
        }
        public Dictionary<string, int> GetWords()
        {
            string text = File.ReadAllText(InputFilePath);
            text = text.ToLower();
            Dictionary<string, int> words = new Dictionary<string, int>();
            foreach (var word in text.Split())
            {
                if (WordsFilter.WordsToFilter.Contains(word)) continue;
                if (!words.Keys.Contains(word)) words[word] = 0;
                words[word]++;
            }

            return words;
        }
    }
}