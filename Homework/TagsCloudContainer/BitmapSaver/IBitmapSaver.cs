using System.Drawing;

namespace TagsCloudContainer.BitmapSaver
{
    public interface IBitmapSaver
    {
        public void Save(Bitmap bmp, string fullPathWithExt);
    }
}