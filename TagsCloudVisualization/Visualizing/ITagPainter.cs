using System.Drawing;

namespace TagsCloudVisualization.Visualizing
{
    public interface ITagPainter
    {
        Brush ChooseBrushForTag(Tag tag);
    } 
}
