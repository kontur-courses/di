using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings;
using TagsCloudVisualisation.App.CloudDrawers.WordToDraw;
using TagsCloudVisualisation.App.DrawingSettings;

namespace TagsCloudVisualisation.App.CloudDrawers
{
    public interface ICloudDrawer
    {
        Bitmap DrawWords(List<IDrawingWord> words, IDrawingSettings settings);
    }
}