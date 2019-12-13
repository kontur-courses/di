using System.Drawing;

namespace TagsCloudApp.ImageSave
{
    public class Saver : IImageSaver
    {
        public void SaveImage(Bitmap bitmap, string filename)
        {            
            bitmap.Save(filename);
        }
    }
}
