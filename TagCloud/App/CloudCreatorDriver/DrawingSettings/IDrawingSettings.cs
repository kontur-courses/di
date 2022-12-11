using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.DrawingSettings;

public interface IDrawingSettings
{
   Color BgColor { get; set; }
        
    List<IWordVisualisation> Visualisations { get; }
        
    Size PictureSize { get; set; }

    void AddVisualisation(IWordVisualisation wordVisualisation);
}