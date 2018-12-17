using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Drawing
{
    public interface IDrawer
    {
        Bitmap Draw(WordLayout layout, ImageSettings settings, out Graphics graphics);
    }
}
