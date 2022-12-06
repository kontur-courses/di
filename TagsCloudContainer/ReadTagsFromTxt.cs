using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public class ReadTagsFromTxt: IReadTags
    {
        public Dictionary<string, int> ReadTagsFromFile(string path)
        {
            var text = File.ReadAllText(path);
            return text
                .ToLower()
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                //научиться убирать скучные слова
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
