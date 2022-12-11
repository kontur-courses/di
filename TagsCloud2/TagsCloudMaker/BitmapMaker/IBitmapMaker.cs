using System.Drawing;

namespace TagsCloud2;

public interface IBitmapMaker
{
    public Bitmap MakeBitmap(Size size, Dictionary<string, TextOptions> stringSizeAndOrientation,
        string fontFamilyName, Brush color);
}