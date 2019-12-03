using System.Collections.Generic;

namespace TagCloud.TextFilter
{
    public class BlacklistSettings
    {
        public HashSet<string> FilesWithBannedWords = new HashSet<string>
        {
            @"..\..\Blacklists\blacklist.txt"
        };
    }
}