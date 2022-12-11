using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.DrawingSettings;

public class DrawingSettings : IDrawingSettings
{
    public Color BgColor { get; set; }
    public List<IWordVisualisation> Visualisations { get; } = new List<IWordVisualisation>();
    public Size PictureSize { get; set; }
        
    public void AddVisualisation(IWordVisualisation wordVisualisation)
    {
        Visualisations.Add(wordVisualisation);
    }
}