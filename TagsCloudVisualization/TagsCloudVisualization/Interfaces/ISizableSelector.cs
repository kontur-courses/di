using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ISizableSelector<T, Tinfo>
    {
        Size GetSize(T obj, Tinfo info);
    }
}