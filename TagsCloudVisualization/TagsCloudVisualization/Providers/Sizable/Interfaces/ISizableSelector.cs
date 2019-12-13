using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Sizable.Interfaces
{
    public interface ISizableSelector<in T, in Tinfo>
    {
        Size GetSize(T obj, Tinfo info, DrawerSettings settings);
    }
}