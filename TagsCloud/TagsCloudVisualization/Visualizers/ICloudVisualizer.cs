using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.Themes;


namespace TagsCloudVisualization.Visualizers
{
    public interface ICloudVisualizer
    {
        Bitmap Visualize(Theme theme, IEnumerable<RectangleF> rectangles,
            int width = 1000, int height = 1000);
        
        Bitmap Visualize(Style style, IEnumerable<Tag> tags,
            int width = 1000, int height = 1000);
    }
}