using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class BackgroundSettings
{
    public Color BackgroundColor { get; }
    
    public BackgroundSettings(string backgroundColor) 
    {
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