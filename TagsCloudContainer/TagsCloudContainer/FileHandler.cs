using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class FileHandler
    {
        private readonly IDullWordsEliminator dullWordsEliminator;

        public FileHandler(IDullWordsEliminator dullWordsEliminator)
        {
            this.dullWordsEliminator = dullWordsEliminator;
        }

        public Dictionary<string, int> GetWordsFrequencyDict(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File with such name is not found");
            var result = new Dictionary<string, int>();
            string pattern = @"\b[a-zA-Z]+";
            foreach (var line in File.ReadLines(fileName))
            {
                foreach (Match match in Regex.Matches(line, pattern))
                {
                    var currentWord = match.Value.ToLower();
                    if (!dullWordsEliminator.IsDull(currentWord))
                        result[currentWord] = result.ContainsKey(currentWord) ? result[currentWord] + 1 : 1;
                }
            }
            return result;
        }
    }
}