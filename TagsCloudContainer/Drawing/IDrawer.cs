using System.Drawing;
using TagsCloudContainer.Geom;

namespace TagsCloudContainer.Drawing
{
    public interface IDrawer
    {
        Bitmap Draw(CircularCloudLayouter layouter, int imageWidth, int imageHeight);
    }
}
