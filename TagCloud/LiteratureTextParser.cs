using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeCantSpell.Hunspell;

namespace TagCloud
{
    public class LiteratureTextParser : IWordParser
    {
        private IPathCreater creater;
            
        public LiteratureTextParser(IPathCreater creater)
        {
            this.creater = creater;
        }
        
        public string[] GetWords(string inputFileName)
        {
            var path = creater.GetCurrentPath();
            var dictionary = WordList.CreateFromFiles(path + "ru_RU.dic", path + "ru_RU.aff");
            var lines = File.ReadLines(path + inputFileName);
            var words = new List<string>();
            foreach (var line in lines)
            {
                words.AddRange(line.Split(' ', '.', ',', ':', '!'));
            }
            
            return words
                .Select(word => dictionary.ContainsEntriesForRootWord(word)
                    ? word
                    : dictionary.CheckDetails(word).Root)
                .Where(word => dictionary.ContainsEntriesForRootWord(word))
                .Where(str => str.Length > 2)
                .Select(word => word.ToLower())
                .ToArray();
        }
    }
}