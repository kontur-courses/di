using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordsToSizesConverter
    {
        int MaxHeight { get; set; }
        int MaxWidth { get; set; }
        Size GetSizeOf(string word);
        IEnumerable<(string, Size)> GetSizesOf(IDictionary<string, int> dict);
    }
}