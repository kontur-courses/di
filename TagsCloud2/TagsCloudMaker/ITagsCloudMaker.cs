using System.Drawing;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TagsCloud2.TagsCloudMaker;

public interface ITagsCloudMaker
{
    public Bitmap MakeTagsCloud(List<WordFrequency> frequency,
        string fontFamilyName, int bigFontSize,
        Brush color,
        Size bitmapSize,
        IBitmapTagsCloudMaker bitmapTagsCloudMaker,
        ISizeDefiner sizeDefiner,
        bool withVertical);
}