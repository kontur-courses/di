using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public interface IWordVisualisation
{
    Color Color { get; }
        
    double StartingValue { get; }
        
    Font Font { get; }
}