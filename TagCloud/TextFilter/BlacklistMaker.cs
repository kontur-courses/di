using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud.TextFilter
{
    public class BlacklistMaker
    {
        private readonly TextFilterSettings filterSettings;
        private readonly BlacklistSettings blacklistSettings;
        public readonly HashSet<string> BlackList = new HashSet<string>();

        public BlacklistMaker(TextFilterSettings filterSettings, BlacklistSettings blacklistSettings)
        {
            this.filterSettings = filterSettings;
            this.blacklistSettings = blacklistSettings;
            CreateBlackList();
        }

        private void CreateBlackList()
        {
            foreach (var filePath in blacklistSettings.FilesWithBannedWords)
                FillBlackListFromFile(filePath);
        }

        private void FillBlackListFromFile(string filePath)
        {
            using (var sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var words = line.Split(filterSettings.Separators);
                    foreach (var word in words.Select(s => s.MakeFirstLetterLowerCase()))
                    {
                        BlackList.Add(word);
                        Console.WriteLine(word);
                    }
                }
            }
        }
    }
}