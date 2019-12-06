using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class FileHandler
    {
        public static Dictionary<string, int> GetWordsFrequencyDict(string fileName, Predicate<string> isValidWord)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File with such name is not found");
            var result = new Dictionary<string, int>();
            foreach (var line in File.ReadLines(fileName))
            {
                if (isValidWord(line))
                {
                    var currentWord = line.ToLower();
                    result[currentWord] = result.ContainsKey(currentWord) ? result[currentWord] + 1 : 1;
                }
            }
            return result;
        }
    }
}