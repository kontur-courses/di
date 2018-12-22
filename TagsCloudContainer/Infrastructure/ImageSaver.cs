using System.Drawing;

namespace TagsCloudContainer
{
    public class ImageSaver
    {
        public void Save(Bitmap image, string filename)
        {
            image.Save(filename);
        }
    }
}