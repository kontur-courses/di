using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;

namespace HomeExerciseTDD
{
    public class FileProcessor : IFileProcessor
    {
        private HashSet<string> boringWords;

        public FileProcessor(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }
        
        public Dictionary<string, int> GetWords(string path)
        {
            //??
            var wordFrequency = new Dictionary<string, int>();
            var word = string.Empty;

            using StreamReader fileReader = new StreamReader(path);
            word = fileReader.ReadLine().ToLower();
            if(!boringWords.Contains(word))    
                wordFrequency[word]++;

            return wordFrequency;
        }
    }
}