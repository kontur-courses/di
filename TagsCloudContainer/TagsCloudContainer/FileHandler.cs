using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class FileHandler
    {
        private readonly IDullWordsEliminator dullWordsEliminator;
        private static readonly Regex wordPattern = new Regex(@"\b[a-zA-Z]+", RegexOptions.Compiled);

        public FileHandler(IDullWordsEliminator dullWordsEliminator)
        {
            this.dullWordsEliminator = dullWordsEliminator;

        }

        public Dictionary<string, int> GetWordsFrequencyDict(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(string.Format("Text file {0} is not found", fileName));
            var result = new Dictionary<string, int>();
            foreach (var line in File.ReadLines(fileName))
            {
                foreach (Match match in wordPattern.Matches(line))
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