using System;
using System.IO;
using System.Linq;
using WeCantSpell.Hunspell;

namespace TagCloud
{
    public class LiteratureTextParser : IWordParser
    {
        private IPathCreater creater;
        private static readonly char[] separators = {' ', '.', ',', ':', '!'};
        private const int minWordLength = 3;
        
            
        public LiteratureTextParser(IPathCreater creater)
        {
            this.creater = creater;
        }
        
        public string[] GetWords(string inputFileName)
        {
            var path = creater.GetCurrentPath();
            var dictionary = GetDictionary(path);
            var lines = File.ReadLines(path + inputFileName);

            return lines
                .SelectMany(line => line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                .Select(word => word.ToLower())
                .Select(word => GetRootForWord(word, dictionary))
                .Where(str => !(str is null))
                .Where(str => str.Length > minWordLength)
                .ToArray();
        }

        private string GetRootForWord(string word, WordList dictionary)
        {
            return dictionary.ContainsEntriesForRootWord(word) ? word : dictionary.CheckDetails(word).Root;
        }

        private WordList GetDictionary(string path)
        {
            return WordList.CreateFromFiles(path + "ru_RU.dic", path + "ru_RU.aff");
        }
    }
}