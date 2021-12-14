using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.DrawableContainers
{
    public interface IDrawableContainer
    {
        IEnumerable<IDrawable> GetItems();
        Size GetMinCanvasSize();
    }
}