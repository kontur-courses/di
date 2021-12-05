using System.Collections.Generic;
using System.Drawing;
using TagCloud.Layouters;

namespace TagCloud.Visualizers
{
    public interface IVisualizer
    {
        Bitmap DrawCloud(IEnumerable<Tag> tags, IDrawingSettings drawingSettings);
    }
}