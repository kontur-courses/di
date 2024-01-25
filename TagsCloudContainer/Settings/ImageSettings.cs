using System.Drawing;

namespace TagsCloudContainer.Settings;

public class ImageSettings
{
    public Font Font { get; set; } = new(FontFamily.GenericSerif, 20);

    public string File { get; set; } = "output";
    
    public Color PrimaryColor { get; set; } = Color.Yellow;
    
    public Color SecondaryColor { get; set; } = Color.Blue;

    public Color BackgroundColor { get; set; } = Color.Black;

    public Size ImageSize { get; set; } = new(800, 800);
}