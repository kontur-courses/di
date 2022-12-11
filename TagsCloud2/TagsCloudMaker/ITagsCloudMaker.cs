using System.Drawing;

namespace TagsCloud2;

public interface ITagsCloudMaker
{
    public Bitmap MakeTagsCloud(Dictionary<string, int> frequency,
        string fontFamilyName, int bigFontSize,
        Brush color,
        Size bitmapSize);
}