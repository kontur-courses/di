using System.Drawing;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.Drawers
{
    public interface IDrawer
    {
        Image Draw<T>(ILayoutContainer<T> layoutContainer);
    }
}