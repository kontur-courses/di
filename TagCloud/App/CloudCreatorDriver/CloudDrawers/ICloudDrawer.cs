using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.WordToDraw;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers;

public interface ICloudDrawer
{
    Bitmap DrawWords(List<IDrawingWord> words, IDrawingSettings settings);
}