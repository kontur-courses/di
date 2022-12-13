using System.Drawing;
using System.Drawing.Text;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class RandomColoredDrawerFactory : IDrawerFactory
{
    public IDrawerProvider Build(DrawerSettings drawerSettings)
    {
        if (drawerSettings is not RandomColoredDrawerSettings settings)
            return new EmptyDrawerProvider();
        return new DrawerProvider(settings);
    }

    private class DrawerProvider : IDrawerProvider
    {
        private readonly RandomColoredDrawerSettings settings;

        public DrawerProvider(RandomColoredDrawerSettings settings)
        {
            this.settings = settings;
        }

        public bool CanProvide => true;

        public IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics)
        {
            return new RandomColoredDrawer(settings, graphics, layouterAlgorithmProvider);
        }
    }
}

public class RandomColoredDrawer : IDrawer
{
    private readonly ILayouterAlgorithmProvider algorithmProvider;
    private readonly RandomColoredDrawerSettings settings;
    private readonly Graphics graphics;
    private static readonly Dictionary<FontStyle, List<FontFamily>> FontFamilies;
    private static readonly FontStyle[] FontStyles;

    public RandomColoredDrawer(RandomColoredDrawerSettings settings, Graphics graphics,
        ILayouterAlgorithmProvider algorithmProvider)
    {
        this.settings = settings;
        this.graphics = graphics;
        this.algorithmProvider = algorithmProvider;
    }

    static RandomColoredDrawer()
    {
        FontStyles = Enum.GetValues<FontStyle>();
        var families = new InstalledFontCollection().Families;
        FontFamilies = new Dictionary<FontStyle, List<FontFamily>>();
        foreach (var style in FontStyles)
        {
            FontFamilies[style] = families.Where(x => x.IsStyleAvailable(style)).Take(10).ToList();
            FontFamilies[style].TrimExcess();
        }
    }

    public void DrawCloud(IReadOnlyCollection<CloudWord> cloudWords)
    {
        var tagPropertyItems = GetTagPropertyItems(cloudWords);

        Draw(tagPropertyItems);
    }

    public static Color GetRandomColor()
    {
        var random = Random.Shared;
        return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    private void Draw(
        IEnumerable<(string word, Font font, Rectangle rectangle)> tagPropertyItems)
    {
        graphics.FillRectangle(GetRandomSolidBrush(), settings.ImageRectangle);
        tagPropertyItems.Foreach((item, _) =>
            DrawTag(item.rectangle,
                item.word,
                item.font));
    }

    private static SolidBrush GetRandomSolidBrush()
    {
        return new(GetRandomColor());
    }

    private void DrawTag(
        Rectangle layoutRectangle,
        string word,
        Font textFont)
    {
        var rectangleBorderPen = new Pen(GetRandomSolidBrush(),
            settings.RectangleBorderSize);

        var textStartingPoint = layoutRectangle.Location;

        textStartingPoint.X += settings.RectangleBorderSize;
        textStartingPoint.Y += settings.RectangleBorderSize;

        if (settings.FillRectangles)
            graphics.FillRectangle(GetRandomSolidBrush(), layoutRectangle);

        if (settings.RectangleBorderSize != 0)
            graphics.DrawRectangle(rectangleBorderPen, layoutRectangle);

        graphics.DrawString(word, textFont, GetRandomSolidBrush(), textStartingPoint);
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

    private IEnumerable<(string word, Font font, Rectangle rectangle)> GetTagPropertyItems(
        IEnumerable<CloudWord> cloudWords)
    {
        var cloudWordAndFontSizeTuples = cloudWords
            .Select(x => (word: x.Text,
                fontSize: Math.Clamp(settings.MinimumTextFontSize + x.Occurrences * 3 - 3,
                    settings.MinimumTextFontSize, settings.MaximumTextFontSize)));

        var cloudWordAndFontTuples = cloudWordAndFontSizeTuples
            .Select(x => (x.word,
                font: GetRandomFont(x.fontSize)));

        var bordersSizeAddition =
            new Size(settings.RectangleBorderSize, settings.RectangleBorderSize) * 2;
        var cloudWordAndBlockSizeTuples = cloudWordAndFontTuples
            .Select(x => (x.word, x.font,
                size: graphics.MeasureString(x.word, x.font).ToSize() + bordersSizeAddition));

        var algorithm = algorithmProvider.Provide();

        var wordsAndRectanglesTuples = cloudWordAndBlockSizeTuples
            .Select(x => (x.word, x.font, rectangle: algorithm.PutNextRectangle(x.size)))
            .Where(x => settings.ImageRectangle.Contains(x.rectangle));

        return wordsAndRectanglesTuples;
    }
}