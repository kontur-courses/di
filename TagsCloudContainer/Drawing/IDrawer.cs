using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Drawing
{
    public interface IDrawer
    {
        WordLayout Layout { get; }
        void Draw(Graphics graphics);
    }
}
