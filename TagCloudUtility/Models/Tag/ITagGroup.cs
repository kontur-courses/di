using System.Drawing;

namespace TagCloud.Utility.Models.Tag
{
    public interface ITagGroup
    {
        FrequencyGroup FrequencyGroup { get; }
        Size Size { get; }

        bool Contains(double frequencyCoef);
        Size GetSizeForWord(string word);
    }
}