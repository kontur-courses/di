using System.Drawing;

namespace TagsCloudVisualization.Layouters.CloudLayouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}