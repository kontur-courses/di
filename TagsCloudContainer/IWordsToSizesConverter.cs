using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IWordsToSizesConverter
    {
        Size SizeOfLayout { get; set; }
        int MaxHeight { get; set; }
        int MaxWidth { get; set; }
        Size GetSizeOf(string word);
        IEnumerable<(string, Size)> GetSizesOf();
    }
}