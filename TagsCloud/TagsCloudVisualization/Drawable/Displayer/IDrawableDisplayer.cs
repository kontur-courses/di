using System.Collections.Generic;
using TagsCloudDrawer;

namespace TagsCloudVisualization.Drawable.Displayer
{
    public interface IDrawableDisplayer
    {
        void Display(IEnumerable<IDrawable> drawables);
    }
}