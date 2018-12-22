using System.Collections.Generic;
using System.Text;

namespace WordCloudImageGenerator.Parsing.Extractors
{
    public class SimpleExtractor:IWordExtractor
    {
        public IEnumerable<string> GetWords(string text)
        {
            List<string> words = new List<string>();
            StringBuilder word = new StringBuilder();
            foreach (char ch in text)
            {
                if (char.IsLetter(ch))
                    word.Append(ch);
                else
                {
                    if (word.Length > 1)
                        words.Add(word.ToString());
                    word.Clear();
                }
            }
            if (word.Length > 1)
                words.Add(word.ToString());
            word.Clear();
            return words;
        }
    }
}