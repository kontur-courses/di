using System;
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
            var pathAff = creater.GetCurrentPath() + "ru_RU.aff";
            var pathDic = creater.GetCurrentPath() + "ru_RU.dic";
            var dictionary = WordList.CreateFromFiles(pathDic, pathAff);
            var words = File.ReadAllText(creater.GetCurrentPath() + inputFileName)
                .Split(' ', '.', ',', ':', '!')
                .Where(str => str.Length > 2)
                .Select(word => dictionary.CheckDetails(word).Root)
                .Where(word => dictionary.ContainsEntriesForRootWord(word))
                .Select(word => word.ToLower());
            return words.ToArray();
        }
    }
}