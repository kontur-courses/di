using System.Drawing;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDrawer
    {
        Result<Bitmap> GetBitmap(CloudInfo cloudInfo, DrawerSettings drawerSettings);
    }
}