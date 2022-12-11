using System.Drawing;

namespace TagsCloud2;

public class BitmapMaker : IBitmapMaker
{
    private ILayouter layouter;
    public BitmapMaker(ILayouter layouter)
    {
        this.layouter = layouter;
    }
    public Bitmap MakeBitmap(Size size, Dictionary<string, TextOptions> stringSizeAndOrientation,
        string fontFamilyName, Brush color)
    {
        var tagCloud = new Bitmap(size.Width, size.Height);
        PaintBackground(size, tagCloud);
        using var graphics = Graphics.FromImage(tagCloud);
        foreach (var item in stringSizeAndOrientation)
        {
            var rectangle = layouter.PutNextRectangle(item.Value.Size);
            var position = new PointF(rectangle.X, rectangle.Y);
            using var font =  new Font(fontFamilyName, item.Value.FontSize);
            if (item.Value.Orientation == Orientation.Vertical)
            {
                var stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                graphics.DrawString("firstText", font, color, position, stringFormat);
            }
            else
                graphics.DrawString("firstText", font, color, position);
        }
        return tagCloud;
    }

    private static void PaintBackground(Size size, Bitmap tagCloud)
    {
        for (var x = 0; x < size.Width; x++)
        {
            for (var y = 0; y < size.Height; y++)
            {
                var whiteColor = Color.FromArgb(255, 255, 255);
                tagCloud.SetPixel(x, y, whiteColor);
            }
        }
    }

}