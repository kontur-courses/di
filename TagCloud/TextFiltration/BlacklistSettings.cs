using System.Collections.Generic;

namespace TagCloud.TextFiltration
{
    public class BlacklistSettings
    {
        public HashSet<string> FilesWithBannedWords { get; set; } = new HashSet<string>
        {
            @"..\..\..\TagCloud\Blacklists\blacklist.txt"
        };
    }
}