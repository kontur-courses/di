using System.Drawing;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.FontSettings;

namespace TagsCloudVisualization;

public class TagImage
{
    private readonly Tag tag;
    public Rectangle Bound;
    private readonly IFontSettingsProvider fontSettingsProvider;
    private readonly IColorGenerator colorGenerator;

    public TagImage(Tag tag, Rectangle bound, IFontSettingsProvider fontSettingsProvider,
        IColorGenerator colorGenerator)
    {
        this.tag = tag;
        this.Bound = bound;
        this.fontSettingsProvider = fontSettingsProvider;
        this.colorGenerator = colorGenerator;
    }

    public void Draw(Graphics graphics)
    {
        using var brush = new SolidBrush(colorGenerator.Generate());
        var fontSettings = fontSettingsProvider.GetSettings();
        using var font = new Font(fontSettings.Family, fontSettings.Size);
        DrawText(graphics, font, brush);
    }

    private void DrawText(Graphics graphics, Font font, Brush brush)
    {
        var textSize = graphics.MeasureString(tag.Text, font);
        var state = graphics.Save();
        graphics.TranslateTransform(Bound.Left, Bound.Top);
        graphics.ScaleTransform(Bound.Width / textSize.Width, Bound.Height / textSize.Height);
        graphics.DrawString(tag.Text, font, brush, PointF.Empty);
        graphics.Restore(state);
    }
}