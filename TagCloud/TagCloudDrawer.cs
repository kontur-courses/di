using System.Drawing;

namespace TagCloud;

public class TagCloudDrawer
{
    private Size imageSize = new(800, 600);

    public Size ImageSize
    {
        get => imageSize;
        set
        {
            if (!value.IsPositive())
                throw new ArgumentException($"Width and height of the image must be positive, but {value}");
            imageSize = value;
        }
    }

    public Color BackgroundColor { get; set; } = Color.White;
    public Pen RectanglesPen { get; set; } = new(Color.Black, 1);

    public Bitmap DrawTagCloud(ICloudLayouter layouter)
    {
        var bitmap = new Bitmap(ImageSize.Width, ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(BackgroundColor);
        graphics.TranslateTransform((float)ImageSize.Width / 2, (float)ImageSize.Height / 2);
        foreach (var rectangle in layouter.Rectangles)
            graphics.DrawRectangle(RectanglesPen, rectangle);
        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

        return bitmap;
    }
}