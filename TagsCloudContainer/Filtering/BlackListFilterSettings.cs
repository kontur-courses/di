using System.Collections.Generic;
using TagsCloudContainer.Reading;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Filters
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