using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud.TextProvider
{
    public class TextFileReader : ITextProvider
    {
        private string FilePath { get; set; } = @"..\..\Input\input.txt";

        private readonly char[] separators = {' ', '"', '(', ')', '.', '!', '?', '\'', ','};

        public Dictionary<string, int> GetParsedText()
        {
            var wordsFrequencyDictionary = new Dictionary<string, int>();
            using (var sr = new StreamReader(FilePath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var words = line.Split(separators);
                    foreach (var word in words.Select(s => s.MakeFirstLetterLowerCase()))
                        if (wordsFrequencyDictionary.ContainsKey(word))
                            wordsFrequencyDictionary[word]++;
                        else wordsFrequencyDictionary[word] = 1;
                }
            }

            return wordsFrequencyDictionary;
        }
    }
}