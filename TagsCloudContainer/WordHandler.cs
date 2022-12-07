using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordHandler
    {
        private readonly HashSet<string> _borringWords;
        
        public WordHandler()
        {
            var text = TextReader.GetTextFromFile("BorringWords.txt");
            _borringWords = new HashSet<string>();
            foreach(var word in text.Split(Environment.NewLine))
                _borringWords.Add(word.ToLower());
        }
        
        public Dictionary<string, int> ProcessWords(string text)
        {
            var words = new Dictionary<string, int>();
            String[] separators = {Environment.NewLine, ", ", ". ", " "};
            foreach (var word in text.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                var wordToDictionary = word.ToLower();
                if(word.Length <= 3 || _borringWords.Contains(wordToDictionary))
                    continue;
                if(!words.ContainsKey(wordToDictionary))
                    words.Add(wordToDictionary, 0);
                words[wordToDictionary]++;
            }

            return words
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);;
        }
    }
}