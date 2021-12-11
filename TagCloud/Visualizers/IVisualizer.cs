using System.Collections.Generic;
using System.Drawing;
using TagCloud.Creators;

namespace TagCloud.Visualizers
{
    public interface IVisualizer
    {
        Bitmap DrawCloud(IEnumerable<Tag> tags, 
            IDrawingSettings drawingSettings, 
            ITagColoring tagColoringAlgorithm);
    }
}