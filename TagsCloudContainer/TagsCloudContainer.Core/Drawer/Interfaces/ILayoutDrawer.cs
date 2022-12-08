using System.Drawing;
using TagsCloudContainer.Core.Layouter;

namespace TagsCloudContainer.Core.Drawer.Interfaces
{
    public interface ILayoutDrawer
    {
        public void AddRectangle(WordRectangle rectangles);

        public void Draw(Graphics graphics);
    }
}
