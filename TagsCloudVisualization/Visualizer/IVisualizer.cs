using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IVisualizer<T>
    {
        Bitmap Draw(IList<T> elements);
    }
}