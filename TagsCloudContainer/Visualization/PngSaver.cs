using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Visualization.Interfaces;

namespace TagsCloudContainer.Visualization
{
    public class PngSaver : ISaver
    {
        public void SaveImage(string path, Bitmap image)
        {
            image.Save(path, ImageFormat.Png);
        }
    }
}