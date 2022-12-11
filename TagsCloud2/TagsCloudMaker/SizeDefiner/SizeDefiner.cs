using System.Drawing;

namespace TagsCloud2;

public class SizeDefiner : ISizeDefiner
{
    private int verticalCount;
    private Random rnd;
    public Dictionary<string, TextOptions> DefineStringSizeAndOrientation(
        Dictionary<string, int> stingAndFontSize, bool withVerticalWords, string fontFamilyName)
    {
        var stringSizeAndOrientation = new Dictionary<string, TextOptions>();
        var bitmap = new Bitmap(50, 50);

        if (withVerticalWords)
        {
            DefineSizeWithVertical(stingAndFontSize, fontFamilyName, bitmap, stringSizeAndOrientation);
        }
        else  DefineSizeOnlyHorizontal(stingAndFontSize, fontFamilyName, bitmap, stringSizeAndOrientation);

        return stringSizeAndOrientation;
    }
    
    private void DefineSizeWithVertical(Dictionary<string, int> stingAndFontSize, 
        string fontFamilyName, 
        Bitmap bitmap,
        Dictionary<string, TextOptions> stringSizeAndOrientation)
    {
        verticalCount = 0;
        rnd = new Random();
        using var graphics = Graphics.FromImage(bitmap);
        foreach (var item in stingAndFontSize)
        {
            var fontSize = item.Value;
            using var font = new Font(fontFamilyName, fontSize);
            var size = graphics.MeasureString(item.Key, font).ToSize();
            var orientation = DefineOrientation();
            if (orientation == Orientation.Vertical)
            {
                var newSize = new Size(size.Height, size.Width);
                size = newSize;
            };
            stringSizeAndOrientation.Add(item.Key, new TextOptions(size, orientation, fontSize));
        }
    }

    private Orientation DefineOrientation()
    {
        var orientation = Orientation.Horizontal;
        var rndNumber = rnd.Next(1, 10);
        var isVertical = rndNumber > 6;
        if (verticalCount >= 10 || !isVertical) return orientation;
        orientation = Orientation.Vertical;
        verticalCount += 1;
        return orientation;
    }

    private void DefineSizeOnlyHorizontal(Dictionary<string, int> stingAndFontSize, 
        string fontFamilyName, 
        Bitmap bitmap,
        Dictionary<string, TextOptions> stringSizeAndOrientation)
    {
        using var graphics = Graphics.FromImage(bitmap);
        foreach (var item in stingAndFontSize)
        {
            var fontSize = item.Value;
            using var font = new Font(fontFamilyName, fontSize);
            var size = graphics.MeasureString(item.Key, font);
            stringSizeAndOrientation.Add(item.Key, new TextOptions(size.ToSize(), Orientation.Horizontal, fontSize));
            // при вертикальной отрисовке надо местами поменть ширину и высоту rec = new Rectangle(50, 50, size.ToSize().Height, size.ToSize().Width);
        }
    }
}