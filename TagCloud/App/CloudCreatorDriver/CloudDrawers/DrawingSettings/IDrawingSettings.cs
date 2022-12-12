using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public interface IDrawingSettings
{
    Color BgColor { get; set; }
    Size PictureSize { get; set; }

    void AddWordVisualisation(IWordVisualisation wordVisualisation);

    IEnumerable<IWordVisualisation> GetWordVisualisations();

    IWordVisualisation GetDefaultVisualisation();
}