using System.Drawing;

namespace TagCloudContainer
{
    public class TagCloudBitmapContainer
    {
        public Bitmap TagCloudBitmap;
        public TagCloudBitmapContainer(Bitmap bitmap)
        {
            TagCloudBitmap = bitmap;
        }

        public void Save(string outputFileName)
        {
            TagCloudBitmap.Save(outputFileName);
        }
    }
}