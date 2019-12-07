using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    internal interface IDrawableProvider<T>
    {
        Point TranslateTransform { get; }
        Size SizeOfCloud { get; }
        IEnumerable<Drawable<T>> DrawableObjects { get; }
    }
}