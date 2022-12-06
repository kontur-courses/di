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
        
        public List<string> ProcessWords(string text)
        {
            return text.Split(Environment.NewLine)
                .Select(word => word.ToLower())
                .Where(word => word.Length > 3 && !_borringWords.Contains(word))
                .ToList();
        }
    }
}