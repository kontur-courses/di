﻿using DeepMorphy;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Algorithm
{
    public class Parser : IParser
    {
        public Dictionary<string, int> CountWordsInFile(string pathToFile)
        {
            var wordsCount = new Dictionary<string, int>();
            using var reader = new StreamReader(pathToFile);
            while (reader.ReadLine()?.Trim().ToLower() is {} line)
                wordsCount[line] = wordsCount.ContainsKey(line) ? wordsCount[line] + 1 : 1;
            return wordsCount;
        }

        public HashSet<string> FindWordsInFile(string pathToFile)
        {
            var words = new HashSet<string>();
            using var reader = new StreamReader(pathToFile);
            while (reader.ReadLine()?.Trim().ToLower() is {} line)
                words.Add(line);
            return words;
        }
    }
}
