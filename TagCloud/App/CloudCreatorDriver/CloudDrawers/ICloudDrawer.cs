using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers;

public interface ICloudDrawer
{
    Bitmap DrawWords(List<IDrawingWord> words, IDrawingSettings settings);
}