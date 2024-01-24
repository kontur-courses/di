using System.Drawing;
using System.Drawing.Text;

public class TagCloudRenderer : ITagCloudRenderer
{
    private readonly RenderOptions options;
    private readonly IColorProvider colorProvider;

    private readonly Size imageSize;
    private readonly Graphics graphics;
    private readonly Bitmap bitmap;
    private readonly Font mainFont;

    public TagCloudRenderer(RenderOptions options, IColorProvider colorProvider)
    {
        imageSize = options.ImageSize;

        bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        graphics = Graphics.FromImage(bitmap);

        var fontFamily = new FontFamily(options.FontFamily);
        mainFont = new Font(fontFamily, 32, FontStyle.Regular, GraphicsUnit.Pixel);

        this.colorProvider = colorProvider;
        this.options = options;
    }

    public Size GetStringSize(string str, int fontSize)
    {
        var newFont = new Font(mainFont.Name, fontSize, mainFont.Style);
        return Size.Truncate(graphics.MeasureString(str, newFont));
    }

    public Bitmap Render(WordLayout[] wordLayouts)
    {
        var rectangles = wordLayouts.Select(x => x.Box).ToArray();

        graphics.Clear(options.ColorScheme.BackgroundColor);

        foreach (var layout in wordLayouts) 
        {
            var brush = new SolidBrush(colorProvider.GetColor(layout));
            var adjFont = new Font(mainFont.Name, layout.FontSize, mainFont.Style);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString(layout.Content, adjFont, brush, layout.Box.Location);
        }

        return bitmap;
    }

    private Font GetAdjustedFont(Graphics graphic, string text, Font originalFont, Size boxSize, int maxFontSize, int minFontSize, bool smallestOnFail)
    {
        for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
        {
            Font testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

            SizeF newSize = graphic.MeasureString(text, testFont);

            if (boxSize.Width > Convert.ToInt32(newSize.Width) && 
                boxSize.Height > Convert.ToInt32(newSize.Height))
            {
                return testFont;
            }
        }

        if (smallestOnFail)
        {
            return new Font(originalFont.Name, minFontSize, originalFont.Style);
        }
        else
        {
            return originalFont;
        }
    }
}