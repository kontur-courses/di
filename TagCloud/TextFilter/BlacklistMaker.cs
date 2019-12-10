using System.Collections.Generic;
using TagCloud.TextProvider;

namespace TagCloud.TextFilter
{
    public class BlacklistMaker
    {
        private readonly ITextParser textParser;
        private readonly BlacklistSettings blacklistSettings;
        public HashSet<string> BlackList = new HashSet<string>();
        public int WordMinLength { get; set; } = 3;

        private string newBlacklistWord;

        public string NewBlacklistWord
        {
            get => newBlacklistWord;
            set => BlackList.Add(value.MakeLettersLowerCase());
        }

        public BlacklistMaker(BlacklistSettings blacklistSettings, ITextParser textParser)
        {
            this.textParser = textParser;
            this.blacklistSettings = blacklistSettings;
            CreateBlackList();
        }

        private void CreateBlackList()
        {
            foreach (var word in textParser.ParseText(blacklistSettings.FilesWithBannedWords))
                BlackList.Add(word);
        }
    }
}