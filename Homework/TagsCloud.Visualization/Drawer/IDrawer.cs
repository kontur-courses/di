using System.Drawing;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.Drawer
{
    public interface IDrawer
    {
        Image Draw<T>(ILayoutContainer<T> layoutContainer);
    }
}