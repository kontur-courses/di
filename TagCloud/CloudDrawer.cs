using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloud.Extensions;
using TagCloud.Words;

namespace TagCloud;

public class CloudDrawer
{
    private readonly IList<Color> _colors;
    private readonly IList<Color> _defaultColors = new List<Color> { Color.MediumVioletRed, Color.Purple, Color.Aqua };
    private readonly Size _size;

    public CloudDrawer(Size size, IEnumerable<Color> colors)
    {
        _size = size;
        _colors = colors.Any() ? colors.ToList() : _defaultColors;
    }

    public Bitmap CreateImage(IList<WordRectangle> words, Font font)
    {
        var image = CreateBitmap();
        DrawWords(image, words, font);
        return image;
    }

    private Bitmap CreateBitmap()
    {
        return new Bitmap(_size.Width, _size.Height);
    }

    private void DrawWords(Bitmap image, IList<WordRectangle> words, Font font)
    {
        var graphics = Graphics.FromImage(image);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        var fontSize = font.Size;
        var random = new Random();
        foreach (var wordRectangle in words)
        {
            var color = GetRandomColorFromColors(random);
            var brush = new SolidBrush(color);
            font = font.ChangeSize(fontSize * wordRectangle.Word.Frequency);
            graphics.DrawString(wordRectangle.Word.Value, font, brush,
                wordRectangle.Rectangle.Location + image.Size.Multiply(0.5));
        }
    }

    private Rectangle GetRectangleOffsetToCenter(Rectangle rectangle, Bitmap image)
    {
        var rectanglePosition = rectangle.Location + image.Size.Multiply(0.5);
        return new Rectangle(rectanglePosition, rectangle.Size);
    }

    private Color GetRandomColor(Random random)
    {
        var r = 25 + random.Next() % 215;
        var g = 25 + random.Next() % 215;
        var b = 25 + random.Next() % 215;

        return Color.FromArgb(r, g, b);
    }

    private Color GetRandomColorFromColors(Random random)
    {
        var randomNumber = random.Next(_colors.Count);
        return _colors[randomNumber];
    }
}