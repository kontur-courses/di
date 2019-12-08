using System.Drawing;


namespace TagsCloudVisualization.TagCloudLayouters
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}
