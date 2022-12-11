namespace TagCloud;

public class CloudDrawer : ICloudDrawer
{
    private Palette Palette { get; set; }
    private SizeProperties Size { get; set; }
    
    public CloudDrawer(SizeProperties size, Palette palette)
    {
        Palette = palette;
        Size = size;
    }

    public Bitmap Draw(IEnumerable<Label> texts)
    {
        var bitmap = new Bitmap(Size.ImageSize.Width, Size.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        using var backgroundBrush = new SolidBrush(Palette.Background);
        graphics.FillRectangle(backgroundBrush, new Rectangle(new Point(0, 0), Size.ImageSize));

        using var brush = new SolidBrush(Palette.Foreground);
        foreach (var textBox in texts)
            graphics.DrawString(textBox.Text, textBox.Font, brush, textBox.Location);

        return bitmap;
    }
}