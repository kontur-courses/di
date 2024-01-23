using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class ImageSettings
{
    public int Width { get; }
    public int Height { get; }
    public Color BackgroundColor { get; }

    public ImageSettings(int width, int height, string backgroundColor)
    {
        Width = width;
        Height = height;
        try
        {
            BackgroundColor = Color.FromName(backgroundColor);
        }
        catch
        {
            throw new ArgumentException($"Color with name {backgroundColor} doesn't supported");
        }
    }

}
