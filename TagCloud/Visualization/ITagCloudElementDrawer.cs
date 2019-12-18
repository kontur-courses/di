using System.Drawing;

namespace TagCloud.Visualization
{
    public interface ITagCloudElementDrawer
    {
        void Draw(Graphics graphics, TagCloudElement element);
    }
}
