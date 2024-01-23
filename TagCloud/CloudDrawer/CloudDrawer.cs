using System.Drawing;
using TagCloud.Layouter;
using TagCloud.Settings;

namespace TagCloud.CloudDrawer;

public class CloudDrawer : IDrawer
{
    private readonly ILayouter layouter;
    private readonly IPalette palette;
    private readonly IAppSettings appSettings;
    private int minimalRank;
    private int maximalRank;
    private const int MaximalFontSize = 50;
    private const int LengthSizeMultiplier = 35;
    private const int ImageBorder = 10;

    public CloudDrawer(ILayouter layouter, IPalette palette, IAppSettings appSettings)
    {
        this.layouter = layouter;
        this.palette = palette;
        this.appSettings = appSettings;
    }
    
    public Bitmap DrawTagCloud(IEnumerable<(string word, int rank)> words)
    {
        var tags = PlaceWords(words);
        var imageSize = GetImageSize(layouter.Rectangles, ImageBorder);
        var shift = GetImageShift(layouter.Rectangles, ImageBorder);
        var image = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(image);
        using var background = new SolidBrush(palette.BackgroudColor);
        graphics.FillRectangle(background, 0, 0, imageSize.Width, imageSize.Height);
        foreach (var tag in tags)
        {
            var shiftedCoordinates = new PointF(tag.Position.X - shift.Width, tag.Position.Y - shift.Height);
            using var brush = new SolidBrush(palette.ForegroundColor);
            graphics.DrawString(tag.Value, new Font(FontFamily.GenericSerif, tag.FontSize), brush, shiftedCoordinates);
        }

        return image;
    }

    private IList<Tag> PlaceWords(IEnumerable<(string word, int rank)> words)
    {
        maximalRank = words.First().rank;
        minimalRank = words.Last().rank - 1;
        
        var tags = new List<Tag>();
        
        foreach (var pair in words)
        {
            var fontSize = CalculateFontSize(pair.rank);
            var boxLength = CalculateWordBoxLength(pair.word.Length, fontSize);
            var rectangle = layouter.PutNextRectangle(new Size(boxLength, fontSize));
            tags.Add(new Tag(pair.word, rectangle, fontSize));
        }

        return tags;
    }

    private int CalculateFontSize(int rank)
    {
        return (MaximalFontSize * (rank - minimalRank)) / (maximalRank - minimalRank);
    }

    private int CalculateWordBoxLength(int length, int fontSize)
    {
        return (int)Math.Round(length * LengthSizeMultiplier * ((double)fontSize / MaximalFontSize));
    }

    private static Size GetImageShift(IList<Rectangle> rectangles, int border)
    {
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var minY = rectangles.Min(rectangle => rectangle.Top);

        return new Size(minX - border, minY - border);
    }

    private static Size GetImageSize(IList<Rectangle> rectangles, int border)
    {
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var maxX = rectangles.Max(rectangle => rectangle.Right);
        var minY = rectangles.Min(rectangle => rectangle.Top);
        var maxY = rectangles.Max(rectangle => rectangle.Bottom);

        return new Size(maxX - minX + 2 * border, maxY - minY + 2 * border);
    }
}