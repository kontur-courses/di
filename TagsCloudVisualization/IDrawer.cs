using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IDrawer
    {
        int GetWidth();
        int GetHeight();

        void Draw(Graphics graphics);
    }
}
