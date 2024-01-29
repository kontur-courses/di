using System.Drawing;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.Visualizers;

public interface IVisualizer
{
    Bitmap Vizualize(IEnumerable<Tag> tags);
}