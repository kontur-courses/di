using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace HomeExerciseTDD
{
    public class FileProcessor : IFileProcessor
    {
        private readonly string pathWords;
        private string pathBoringWords;
        private List<string> exludedWords = new List<string>();
        
        public FileProcessor(string pathWords, string pathBoringWords)
        {
            this.pathWords = pathWords;
            this.pathBoringWords = pathBoringWords;
        }
        
        public Dictionary<string, int> GetWords()
        {
            var wordFrequency = new Dictionary<string, int>();
            var words = File.ReadAllLines(pathWords);
            if(pathBoringWords!=null)
                exludedWords = File.ReadAllLines(pathBoringWords).ToList();

            var formattedWords = words
                .Where(w=>!exludedWords.Contains(w))
                .Select(w => w.ToLower()).ToList();
            foreach (var word in formattedWords)
            {
                wordFrequency[word] = wordFrequency.ContainsKey(word) ? ++wordFrequency[word] : 1;
            }
            
            return wordFrequency;
        }
    }
}