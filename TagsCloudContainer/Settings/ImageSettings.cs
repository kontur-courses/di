using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace TagsCloudContainer.Settings;

public class ImageSettings: IImageSettings
{
    public Color PrimaryColor { get; set; } = Color.Yellow;

    public Color SecondaryColor { get; set; } = Color.Blue;

    public Color BackgroundColor { get; set; } = Color.Black;

    public Size ImageSize { get; set; } = new(1000, 1000);

    public TextOptions TextOptions { get; set; } = new(SystemFonts.CreateFont("Segoe UI", 30, FontStyle.Regular));
}