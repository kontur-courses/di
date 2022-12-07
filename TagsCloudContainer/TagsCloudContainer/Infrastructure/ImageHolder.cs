using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class ImageHolder : PictureBox
    {
        public Size GetImageSize()
        {
            return Image.Size;
        }

        public Graphics StartDrawing()
        {
            return Graphics.FromImage(Image);
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }
    }
}
