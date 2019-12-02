using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class TextPreparer
    {
        private readonly Dictionary<string, int> wordsFrequencyDictionary = new Dictionary<string, int>();
        private readonly TextPreparerSettings settings;

        public TextPreparer(TextPreparerSettings settings)
        {
            this.settings = settings;
            ParseFile();
        }

        private void ParseFile()
        {
            using (var sr = new StreamReader(settings.FilePath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var words = line.Split(' ', '"', '(', ')', '.', '!', '?', '\'', ',');
                    foreach (var word in words.Select(MakeFirstLetterUpperCase))
                    {
                        if (word.Length < 3 || settings.RusWordsBlackList.Contains(word) ||
                            settings.EngWordsBlackList.Contains(word))
                            continue;
                        if (wordsFrequencyDictionary.ContainsKey(word))
                            wordsFrequencyDictionary[word]++;
                        else wordsFrequencyDictionary[word] = 1;
                    }
                }
            }
        }

        private string MakeFirstLetterUpperCase(string word)
        {
            switch (word)
            {
                case null: return string.Empty;
                case "": return string.Empty;
                default: return word.First().ToString().ToUpper() + word.Substring(1);
            }
        }

        public Dictionary<string, int> GetParsedTextDictionary()
        {
            return wordsFrequencyDictionary;
        }
    }
}