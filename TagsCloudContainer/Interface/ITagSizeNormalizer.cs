using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagSizeNormalizer
    {
        Size GetTagSize(string word);
    }
}