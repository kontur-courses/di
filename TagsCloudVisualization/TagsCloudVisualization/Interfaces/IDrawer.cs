using System.Drawing;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDrawer<T>
    {
        Bitmap GetBitmap(CloudInfo<T> sizableSource, DrawerSettings settings);
    }
}