using System.Drawing;

namespace TagsCloudVisualization.BitmapSavers
{
    public interface IBitmapSaver
    {
        void Save(Bitmap bitmap, string path);
    }
}