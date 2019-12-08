using System.Collections.Generic;
using TagCloud.TextProvider;

namespace TagCloud.TextFilter
{
    public class BlacklistMaker
    {
        private readonly ITextProvider textProvider;
        private readonly BlacklistSettings blacklistSettings;
        public HashSet<string> BlackList { get; } = new HashSet<string>();
        public int WordMinLength { get; set; } = 3;

        private string newBlacklistWord;

        public string NewBlacklistWord
        {
            get => newBlacklistWord;
            set => BlackList.Add(value.MakeFirstLetterLowerCase());
        }

        public BlacklistMaker(BlacklistSettings blacklistSettings, ITextProvider textProvider)
        {
            this.textProvider = textProvider;
            this.blacklistSettings = blacklistSettings;
            CreateBlackList();
        }

        private void CreateBlackList()
        {
            foreach (var word in textProvider.GetAllWords(blacklistSettings.FilesWithBannedWords))
                BlackList.Add(word);
        }
    }
}