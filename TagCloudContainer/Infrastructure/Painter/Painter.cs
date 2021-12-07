using System.Drawing;
using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.Layouter;

namespace TagCloudContainer.Infrastructure.Painter;

public class Painter : IPainter
{
    private readonly ICloudLayouter layouter;
    private readonly IPalette palette;
    private readonly IAppSettings settings;

    public Painter(IPalette palette, ICloudLayouter layouter, IAppSettings settings)
    {
        this.palette = palette;
        this.layouter = layouter;
        ValidateSettings(settings);
        this.settings = settings;
    }

    public Bitmap CreateImage(Dictionary<string, int> weightedWords)
    {
        if (weightedWords == null || !weightedWords.Any())
            throw new InvalidOperationException("Impossible to save an empty tag cloud");

        var bitmap = new Bitmap(settings.ImageWidth, settings.ImageHeight);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(palette.BackgroundColor);

        var tags = GetTags(weightedWords, graphics);
        var scale = CalculateScale(settings.ImageWidth, settings.ImageHeight, tags);

        foreach (var tag in tags)
        {
            using var brush = new SolidBrush(palette.MainColor);
            using var font = new Font(settings.FontName, tag.FontSize * scale);
            graphics.DrawString(tag.Word, font, brush, RescaleRectangle(tag.Position, scale));
        }

        return bitmap;
    }

    private List<Tag> GetTags(Dictionary<string, int> weightedWords, Graphics graphics)
    {
        var positionedWords = new List<Tag>();

        foreach (var (word, weight) in weightedWords)
        {
            var fontSize = 10 + weight / 2;
            using var font = new Font(settings.FontName, fontSize);
            var size = graphics.MeasureString(word, font);
            var rectangle = layouter.PutNextRectangle(Size.Ceiling(size));
            positionedWords.Add(new Tag(word, rectangle, fontSize));
        }

        return positionedWords;
    }

    private static void ValidateSettings(IImageSettingsProvider settings)
    {
        if (settings.ImageHeight <= 0 || settings.ImageWidth <= 0)
            throw new ArgumentException($"Image sizes must be great than zero, but was {settings.ImageWidth}x{settings.ImageHeight}");
    }

    private static float CalculateScale(int imageWidth, int imageHeight, IReadOnlyCollection<Tag> tags)
    {
        var layoutSize = CalculateLayoutSize(tags);
        var scale = Math.Min((float)imageHeight / layoutSize.Height, (float)imageWidth / layoutSize.Width);

        return scale < 1 ? scale : 1;
    }

    private static RectangleF RescaleRectangle(RectangleF rectangle, float scale)
    {
        var size = new SizeF(rectangle.Size.Width * scale, rectangle.Size.Height * scale);
        var location = new PointF(rectangle.X * scale, rectangle.Y * scale);

        return new RectangleF(location, size);
    }

    private static Size CalculateLayoutSize(IReadOnlyCollection<Tag> positionedWords)
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