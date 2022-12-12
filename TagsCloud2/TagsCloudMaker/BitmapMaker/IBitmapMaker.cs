using System.Drawing;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TagsCloud2.TagsCloudMaker.BitmapMaker;

public interface IBitmapMaker
{
    public Bitmap MakeBitmap(Size size, Dictionary<string, TextOptions> stringSizeAndOrientation,
        string fontFamilyName, Brush color);
}