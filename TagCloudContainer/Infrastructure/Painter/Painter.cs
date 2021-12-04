using System.Drawing;
using TagCloudContainer.Infrastructure.Layouter;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainer.Infrastructure.Painter;

public class Painter : IPainter
{
    private readonly ICloudLayouter layouter;
    private readonly IPalette palette;

    public Painter(IPalette palette, ICloudLayouter layouter)
    {
        this.palette = palette;
        this.layouter = layouter;
    }

    public Bitmap CreateImage(IEnumerable<WeightedWord> weightedWords, int imageWidth, int imageHeight, string fontName)
    {
        var bitmap = new Bitmap(imageWidth, imageHeight);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(palette.BackgroundColor);

        var positionedWords = GetPositionedWords(weightedWords, fontName, graphics);
        var scale = CalculateScale(imageWidth, imageHeight, positionedWords);

        foreach (var positionedWord in positionedWords)
        {
            using var brush = new SolidBrush(palette.MainColor);
            using var font = new Font(fontName, positionedWord.FontSize * scale);
            graphics.DrawString(positionedWord.Word, font, brush, RescaleRectangle(positionedWord.Position, scale));
        }

        return bitmap;
    }

    private List<PositionedWord> GetPositionedWords(IEnumerable<WeightedWord> weightedWords, string fontName, Graphics graphics)
    {
        var list = new List<PositionedWord>();

        foreach (var weightedWord in weightedWords)
        {
            var fontSize = 10 + weightedWord.Weight / 2;
            using var font = new Font(fontName, fontSize);
            var size = graphics.MeasureString(weightedWord.Word, font);
            var rectangle = layouter.PutNextRectangle(Size.Ceiling(size));
            list.Add(new PositionedWord(weightedWord.Word, rectangle, fontSize));
        }

        return list;
    }

    private static float CalculateScale(int imageWidth, int imageHeight, IReadOnlyCollection<PositionedWord> positionedWords)
    {
        var layoutSize = CalculateLayoutSize(positionedWords);
        var scale = Math.Min((float)imageHeight / layoutSize.Height, (float)imageWidth / layoutSize.Width);

        return scale < 1 ? scale : 1;
    }

    private static RectangleF RescaleRectangle(RectangleF rectangle, float scale)
    {
        var size = new SizeF(rectangle.Size.Width * scale, rectangle.Size.Height * scale);
        var location = new PointF(rectangle.X * scale, rectangle.Y * scale);

        return new RectangleF(location, size);
    }

    private static Size CalculateLayoutSize(IReadOnlyCollection<PositionedWord> positionedWords)
    {
        var maxX = positionedWords.Max(x => x.Position.X + x.Position.Size.Width);
        var maxY = positionedWords.Max(x => x.Position.Y);
        var minX = positionedWords.Min(x => x.Position.X);
        var minY = positionedWords.Min(x => x.Position.Y - x.Position.Size.Height);
        var width = maxX - minX;
        var height = maxY - minY;

        return new Size(width, height);
    }
}