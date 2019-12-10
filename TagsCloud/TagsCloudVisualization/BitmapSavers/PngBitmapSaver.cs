using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.BitmapSavers
{
    public class PngBitmapSaver : IBitmapSaver
    {
        public void Save(Bitmap bitmap, string path) => bitmap.Save($"{path}.png", ImageFormat.Png);
    }
}