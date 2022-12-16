using System.Drawing;

#pragma warning disable CA1416

namespace TagsCloudVisualization;

public class LayoutDrawer
{
    public void Draw(DrawingModel drawingModel)
    {
        var rectangles = drawingModel.WordsToSizes.Values.ToList();
        var bitmap = new Bitmap(drawingModel.FieldWidth, drawingModel.FieldHeight);
        var graphics = Graphics.FromImage(bitmap);

        var stringFormat = GetCentredStringFormat();

        foreach (var key in drawingModel.WordsToSizes.Keys)
        {
            var centredRectangle = 
                GetCentredRectangle(drawingModel.WordsToSizes[key], rectangles[0].Location, drawingModel.FieldWidth, drawingModel.FieldHeight);

            graphics.DrawString(
                key,
                GetResizedFont(graphics, drawingModel.FontName, key, centredRectangle),
                GetRandomBrush(drawingModel.Colors),
                centredRectangle,
                stringFormat);
        }

        bitmap.Save(drawingModel.FilePath);
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


    private Rectangle GetCentredRectangle(Rectangle rectangle, Point center, int fieldWidth, int fieldHeight)
    {
        var X = rectangle.Location.X - center.X + fieldWidth / 2;
        var Y = rectangle.Location.Y - center.Y + fieldHeight / 2;
        rectangle.Location = new Point(X, Y);
        return rectangle;
    }
}
#pragma warning restore CA1416