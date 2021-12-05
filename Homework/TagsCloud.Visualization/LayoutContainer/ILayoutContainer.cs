using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Visualization.LayoutContainer
{
    public interface ILayoutContainer<out T> : IVisitable
    {
        IEnumerable<T> Items { get; }
        (int width, int height) GetWidthAndHeight();
        Point GetCenter();
    }
}