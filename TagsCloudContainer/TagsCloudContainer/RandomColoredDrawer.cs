using System.Drawing;
using System.Drawing.Text;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class RandomColoredDrawer : IDrawer
{
    private static readonly Dictionary<FontStyle, List<FontFamily>> FontFamilies;
    private static readonly FontStyle[] FontStyles;
    private readonly ILayouterAlgorithmProvider algorithmProvider;
    private readonly Graphics graphics;
    private readonly RandomColoredDrawerSettings settings;

    static RandomColoredDrawer()
    {
        FontStyles = Enum.GetValues<FontStyle>();
        var families = new InstalledFontCollection().Families;
        FontFamilies = new();
        foreach (var style in FontStyles)
        {
            FontFamilies[style] = families.Where(x => x.IsStyleAvailable(style)).Take(10).ToList();
            FontFamilies[style].TrimExcess();
        }
    }

    public RandomColoredDrawer(RandomColoredDrawerSettings settings, Graphics graphics,
        ILayouterAlgorithmProvider algorithmProvider)
    {
        this.settings = settings;
        this.graphics = graphics;
        this.algorithmProvider = algorithmProvider;
    }

    public void DrawCloud(IReadOnlyCollection<CloudWord> cloudWords)
    {
        var algorithm = algorithmProvider.Provide();

        var tagPropertyItems = cloudWords.GetTagDrawingItems(algorithm,
            GetFont,
            SizeResolver,
            Filter);

        Draw(tagPropertyItems);
    }

    private bool Filter(TagDrawingItem tag)
    {
        return settings.ImageRectangle.Contains(tag.Rectangle);
    }

    private Size SizeResolver(TagDrawingItem tag)
    {
        var size = graphics.MeasureString(tag.Text, tag.Font).ToSize();
        size.Width += settings.RectangleBorderSize * 2;
        size.Height += settings.RectangleBorderSize * 2;
        return size;
    }

    private Font GetFont(CloudWord cloudWord)
    {
        var fontSize = Math.Clamp(settings.MinimumTextFontSize + cloudWord.Occurrences * 3 - 3,
            settings.MinimumTextFontSize, settings.MaximumTextFontSize);
        return GetRandomFont(fontSize);
    }

    public static Color GetRandomColor()
    {
        var random = Random.Shared;
        return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    private void Draw(
        IEnumerable<TagDrawingItem> tagPropertyItems)
    {
        graphics.FillRectangle(GetRandomSolidBrush(), settings.ImageRectangle);
        tagPropertyItems.ForeachWithCounterFromZero((item, _) =>
            DrawTag(item));
    }

    private void DrawTag(TagDrawingItem item)
    {
        var rectangleBorderPen = new Pen(GetRandomSolidBrush(),
            settings.RectangleBorderSize);

        var textStartingPoint = item.Rectangle.Location;

        textStartingPoint.X += settings.RectangleBorderSize;
        textStartingPoint.Y += settings.RectangleBorderSize;

        if (settings.FillRectangles)
            graphics.FillRectangle(GetRandomSolidBrush(), item.Rectangle);

        if (settings.RectangleBorderSize != 0)
            graphics.DrawRectangle(rectangleBorderPen, item.Rectangle);

        graphics.DrawString(item.Text, item.Font, GetRandomSolidBrush(), textStartingPoint);
    }

    private static SolidBrush GetRandomSolidBrush()
    {
        return new(GetRandomColor());
    }

    private static T GetRandom<T>(IReadOnlyList<T> source)
    {
        return source[Random.Shared.Next(0, source.Count)];
    }

    private static Font GetRandomFont(float fontSize)
    {
        fontSize = Random.Shared.Next((int)Math.Round(fontSize), (int)fontSize * 2);
        var fontStyle = GetRandom(FontStyles);
        var fontFamily = GetRandom(FontFamilies[fontStyle]);
        return new(fontFamily, fontSize, fontStyle);
    }
}