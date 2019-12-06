using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Core.ImageSavers
{
    class PngSaver : IImageSaver
    {
        public void Save(string pathImage, Bitmap bitmap)
        { 
            bitmap.Save(pathImage, ImageFormat.Png);
            Console.WriteLine($"Tag cloud visualization saved to file {pathImage}");
        }
    }
}
