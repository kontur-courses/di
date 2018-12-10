using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TagsCloudContainer
{
    public class BlackListFilterSettings
    {
        public HashSet<string> Blacklist;

        public BlackListFilterSettings(IUI ui)
        {
            Blacklist = new HashSet<string>(new TxtWordsReader().ReadWords(ui.BlacklistPath));
        }
    }
}