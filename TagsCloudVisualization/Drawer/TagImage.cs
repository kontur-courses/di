using System.Drawing;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.FontSettings;

namespace TagsCloudVisualization.Drawer;

public class TagImage : IDrawImage
{
    private readonly Tag tag;
    public Rectangle Bounds { get; }
    private readonly IFontSettingsProvider fontSettingsProvider;
    private readonly IColorGenerator colorGenerator;

    public TagImage(Tag tag, Rectangle bounds, IFontSettingsProvider fontSettingsProvider,
        IColorGenerator colorGenerator)
    {
        this.tag = tag;
        this.Bounds = bounds;
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

    public IDrawImage Offset(Size size)
    {
        return new TagImage(tag, new Rectangle(Bounds.Location + size, Bounds.Size), fontSettingsProvider,
            colorGenerator);
    }

    private void DrawText(Graphics graphics, Font font, Brush brush)
    {
        var textSize = graphics.MeasureString(tag.Text, font);
        var state = graphics.Save();
        graphics.TranslateTransform(Bounds.Left, Bounds.Top);
        graphics.ScaleTransform(Bounds.Width / textSize.Width, Bounds.Height / textSize.Height);
        graphics.DrawString(tag.Text, font, brush, PointF.Empty);
        graphics.Restore(state);
    }
}