using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Vizualization
{
    public class PngSaver : ISaver
    {
        public void SaveImage(string path, Bitmap image)
        {
            image.Save(path, ImageFormat.Png);
        }
    }
}