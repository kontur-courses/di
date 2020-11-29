using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class WordsHandler : IWordsHandler
    {
        private readonly HashSet<string> boringWords;
        
        public WordsHandler(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }
        
        public List<string> HandleWords(List<string> words)
        {
            var clearedWords = new List<string>();
            foreach (var word in words)
            {
                var loweredWord = word.ToLower();
                if(boringWords.Contains(loweredWord))
                    continue;
                clearedWords.Add(loweredWord);
                
            }
            return clearedWords;
        }
    }
}