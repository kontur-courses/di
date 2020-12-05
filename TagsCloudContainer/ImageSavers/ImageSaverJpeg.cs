using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudContainer
{
    class ImageSaverJpeg : IImageSaver
    {
        public string Format { get; set; }

        public ImageSaverJpeg()
        {
            Format = "jpeg";
        }

        public void Save(string path, string name, Bitmap bitmap)
        {
            bitmap.Save(path + "\\" + name + "." + Format, ImageFormat.Jpeg);
        }
    }
}
