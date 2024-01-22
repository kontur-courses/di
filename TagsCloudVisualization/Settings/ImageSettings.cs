using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class ImageSettings
{
    public int Width { get; }
    public int Height { get; }
    public Color BackgroundColor { get; } = Color.White;

    public ImageSettings(int width = 1920, int height = 1080) 
    {
        Width = width;
        Height = height;
    }

    public ImageSettings(Color backgroundColor, int width = 1920, int height = 1080)
    {
        Width = width;
        Height = height;
        BackgroundColor = backgroundColor;
    }

}
