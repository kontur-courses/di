using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Sizable.Interfaces
{
    public interface ISizableSelector
    {
        Size GetSize(string word, int count, DrawerSettings settings);
    }
}