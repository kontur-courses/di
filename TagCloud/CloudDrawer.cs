using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloud.Extensions;
using TagCloud.Words;

namespace TagCloud;

public class CloudDrawer
{
    private readonly IList<Color> _defaultColors = new List<Color> { Color.Black };

    public CloudDrawer()
    {
        
    }

    public Bitmap CreateImage(IList<WordRectangle> words, Size size, Font font, IEnumerable<Color> colors)
    {
        var image = CreateBitmap(size);
        colors = colors.Any() ? colors : _defaultColors;
        DrawWords(image, words, font, colors);
        return image;
    }

    private Bitmap CreateBitmap(Size size)
    {
        return new Bitmap(size.Width, size.Height);
    }

    private void DrawWords(Bitmap image, IList<WordRectangle> words, Font font, IEnumerable<Color> colors)
    {
        var graphics = Graphics.FromImage(image);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        var fontSize = font.Size;
        var random = new Random();
        
        foreach (var wordRectangle in words)
        {
            var color = GetRandomColorFromColors(colors, random);
            var brush = new SolidBrush(color);
            font = font.ChangeSize(fontSize * wordRectangle.Word.Frequency);
            graphics.DrawString(wordRectangle.Word.Value, font, brush,
                wordRectangle.Rectangle.Location + image.Size.Multiply(0.5));
        }
    }

    private Color GetRandomColorFromColors(IEnumerable<Color> colors, Random random)
    {
        var colorsList = colors.ToList();
        var randomNumber = random.Next(colorsList.Count);
        return colorsList[randomNumber];
    }
}