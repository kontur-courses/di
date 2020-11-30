using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
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
            var lines = File.ReadLines(creater.GetCurrentPath() + inputFileName);
            var words = new List<string>();
            foreach (var line in lines)
            {
                words.AddRange(line.Split(' ', '.', ',', ':', '!'));
            }
            
            return words
                .Where(str => str.Length > 2)
                .Select(word => dictionary.ContainsEntriesForRootWord(word)
                    ? word
                    : dictionary.CheckDetails(word).Root)
                .Where(word => dictionary.ContainsEntriesForRootWord(word))
                .Select(word => word.ToLower())
                .ToArray();
        }
    }
}