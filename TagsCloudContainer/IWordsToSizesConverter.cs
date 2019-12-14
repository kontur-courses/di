using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordsToSizesConverter
    {
        Size Size { get; set; }
        int MaxHeight { get; set; }
        int MaxWidth { get; set; }
        IEnumerable<(string, Size)> GetSizesOf(Dictionary<string, int> dictionary);
    }
}