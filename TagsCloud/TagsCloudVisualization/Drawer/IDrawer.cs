using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public interface IDrawer
    {
        void Draw(Graphics graphics, Size size, IEnumerable<IDrawable> drawables);
    }
}