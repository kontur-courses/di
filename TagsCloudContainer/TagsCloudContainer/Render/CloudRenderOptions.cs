using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render;

public abstract class CloudRenderOptions
{
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color[] TextColors { get; set; } = {Color.Aqua, Color.Bisque, Color.Red, Color.YellowGreen, Color.Coral, Color.Purple};
    public int MinimumFontSize { get; set; } = 20;
    public string FontName { get; set; } = "Arial";
    public int ImageWidth { get; set; } = 4096;
    public int ImageHeight { get; set; } = 4096;

    public Color NextTextColor => TextColors.OrderBy(_ => Guid.NewGuid()).First();
}