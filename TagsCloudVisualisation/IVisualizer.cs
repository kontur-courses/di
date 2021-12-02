using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IVisualizer
{
    Bitmap GetBitmap(ITagPacker tags, ILayouter layouter, IStyler styler);
}