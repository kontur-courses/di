using System.Drawing;

#pragma warning disable CA1416

namespace TagsCloudVisualization;

public class LayoutDrawer
{
    public void Draw(Dictionary<string, Rectangle> wordsToSizes, string? filename, Size fieldSize,
        HashSet<Brush> colors, string? fontName)
    {
        var rectangles = wordsToSizes.Values.ToList();
        var bitmap = new Bitmap(fieldSize.Width, fieldSize.Height);
        var graphics = Graphics.FromImage(bitmap);

        var stringFormat = GetCentredStringFormat();

        foreach (var key in wordsToSizes.Keys)
        {
            var centredRectangle = GetCentredRectangle(wordsToSizes[key], rectangles[0].Location, fieldSize);

            graphics.DrawString(
                key,
                GetResizedFont(graphics, fontName, key, centredRectangle),
                GetRandomBrush(colors),
                centredRectangle,
                stringFormat);
        }

        bitmap.Save(filename);
    }

    private Brush GetRandomBrush(HashSet<Brush> colors)
    {
        var i = new Random().Next(0, colors.Count - 1);
        return colors.ToList()[i];
    }

    private Font GetResizedFont(Graphics graphics, string? fontName, string text, Rectangle rectangle)
    {
        var primarySize = graphics.MeasureString(text, new Font(fontName, 1));
        var width = rectangle.Width / primarySize.Width;
        var height = rectangle.Height / primarySize.Height;

        return width <= height ? new Font(fontName, width) : new Font(fontName, height);
    }

    private StringFormat GetCentredStringFormat()
    {
        var textFormat = new StringFormat();
        textFormat.Alignment = StringAlignment.Center;
        textFormat.LineAlignment = StringAlignment.Center;
        return textFormat;
    }


    private Rectangle GetCentredRectangle(Rectangle rectangle, Point center, Size fieldSize)
    {
        var X = rectangle.Location.X - center.X + fieldSize.Width / 2;
        var Y = rectangle.Location.Y - center.Y + fieldSize.Height / 2;
        rectangle.Location = new Point(X, Y);
        return rectangle;
    }
}
#pragma warning restore CA1416