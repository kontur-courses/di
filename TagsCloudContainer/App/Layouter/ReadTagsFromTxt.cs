using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.App.Layouter
{
    public class TagsReaderFromTxt : ITagsReader
    {
        public Dictionary<string, int> Text { get; set; }

        public void ReadTagsFromFile(string path)
        {
            var text = File.ReadAllText(path);
            Text = text
                .ToLower()
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                //научиться убирать скучные слова
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
