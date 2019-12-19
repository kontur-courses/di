using System.Drawing;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDrawer
    {
        Bitmap GetBitmap(CloudInfo cloudInfo, DrawerSettings drawerSettings);
    }
}