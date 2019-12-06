using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    internal interface IDrawableProvider<T>
    {
        Size SizeOfCloud { get; }
        IEnumerable<Drawable<T>> DrawableObjects { get; }
    }
}