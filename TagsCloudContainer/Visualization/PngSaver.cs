using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Visualization.Interfaces;

namespace TagsCloudContainer.Visualization
{
    public class PngSaver : ISaver
    {
        public void SaveImage(string path, Bitmap image, Size resolution)
        {
            var resizedImage = ResizeBitmap(image, resolution.Width, resolution.Height);
            resizedImage.Save(path, ImageFormat.Png);
        }

        private Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }
    }
}