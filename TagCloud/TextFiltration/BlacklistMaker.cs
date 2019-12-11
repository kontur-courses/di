using System.Collections.Generic;
using TagCloud.TextParser;

namespace TagCloud.TextFiltration
{
    public class BlacklistMaker
    {
        private readonly ITextParser textParser;
        private readonly BlacklistSettings blacklistSettings;
        public HashSet<string> BlackList { get; private set; } = new HashSet<string>();

        public BlacklistMaker(BlacklistSettings blacklistSettings, ITextParser textParser)
        {
            this.textParser = textParser;
            this.blacklistSettings = blacklistSettings;
            CreateBlackList();
        }

        public void CreateBlackList()
        {
            BlackList = new HashSet<string>();
            foreach (var word in textParser.ParseText(blacklistSettings.FilesWithBannedWords))
                BlackList.Add(word);
        }
    }
}