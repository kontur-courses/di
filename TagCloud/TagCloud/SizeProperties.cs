namespace TagCloud;

public class SizeProperties
{
    public Size ImageSize { get; set; } = new(1024, 1024);
    public Point ImageCenter => new(ImageSize.Width / 2, ImageSize.Height / 2);
}