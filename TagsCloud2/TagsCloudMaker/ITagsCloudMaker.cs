using System.Drawing;

namespace TagsCloud2;

public interface ITagsCloudMaker
{
    public Bitmap MakeTagsCloud(List<WordFrequency> frequency,
        string fontFamilyName, int bigFontSize,
        Brush color,
        Size bitmapSize,
        IBitmapMaker bitmapMaker,
        ISizeDefiner sizeDefiner,
        bool withVertical);
}