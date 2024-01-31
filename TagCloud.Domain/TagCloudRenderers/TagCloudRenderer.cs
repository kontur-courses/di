using System.Drawing;
using System.Drawing.Text;

public class TagCloudRenderer : ITagCloudRenderer
{
    private readonly RenderOptions options;
    private readonly IColorProvider colorProvider;

    private readonly Size imageSize;
    private readonly Graphics graphics;
    private readonly Bitmap bitmap;
    private readonly Font fontBase;

    public TagCloudRenderer(RenderOptions options, IColorProvider colorProvider)
    {
        imageSize = options.ImageSize;

        bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        graphics = Graphics.FromImage(bitmap);

        fontBase = options.FontBase;

        this.colorProvider = colorProvider;
        this.options = options;
    }

    public Size GetStringSize(string str, int fontSize)
    {
        var newFont = new Font(fontBase.Name, fontSize, fontBase.Style);
        return Size.Truncate(graphics.MeasureString(str, newFont));
    }

    public Bitmap Render(WordLayout[] wordLayouts)
    {
        graphics.Clear(options.ColorScheme.BackgroundColor);

        foreach (var layout in wordLayouts) 
        {
            var brush = new SolidBrush(colorProvider.GetColor(layout));
            var adjFont = new Font(fontBase.Name, layout.FontSize, fontBase.Style);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString(layout.Content, adjFont, brush, layout.Box.Location);
        }

        return bitmap;
    }
}