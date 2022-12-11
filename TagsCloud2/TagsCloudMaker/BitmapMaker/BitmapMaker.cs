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
        var centralPoint = size.Height / 2;
        if (centralPoint % 2 == 1)
            centralPoint += 1;
        using var graphics = Graphics.FromImage(tagCloud);
        var rnd = new Random();
        var shuffledDictionary = stringSizeAndOrientation.OrderBy(
            x => rnd.Next()).ToDictionary(
            item => item.Key, item => item.Value);
        foreach (var item in shuffledDictionary)
        {
            var rectangle = layouter.PutNextRectangle(item.Value.Size);
            var position = new PointF(rectangle.X + centralPoint, rectangle.Y + centralPoint);
            using var font = new Font(fontFamilyName, item.Value.FontSize);
            if (item.Value.Orientation == Orientation.Vertical)
            {
                var stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                graphics.DrawString(item.Key, font, color, position, stringFormat);
            }
            else
                graphics.DrawString(item.Key, font, color, position);
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