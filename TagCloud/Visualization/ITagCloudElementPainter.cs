using System.Drawing;

namespace TagCloud.Visualization
{
    public interface ITagCloudElementPainter
    {
        void Paint(Graphics graphics, TagCloudElement element);
    }
}
