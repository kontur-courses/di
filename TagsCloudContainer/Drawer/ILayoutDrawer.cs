using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public interface ILayoutDrawer
    {
        internal Bitmap Bitmap { get; }

        internal Graphics Graphics { get; }

        public void AddRectangles(IEnumerable<WordRectangle> rectangles);

        public void Draw();
    }
}