using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.BitmapSavers
{
    public class WmfBitmapSaver : IBitmapSaver
    {
        public void Save(Bitmap bitmap, string path) => bitmap.Save($"{path}.wmf", ImageFormat.Wmf);
    }
}