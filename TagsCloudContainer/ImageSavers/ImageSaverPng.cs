using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudContainer
{
    class ImageSaverPng : IImageSaver
    {
        public string Format { get; set; }

        public ImageSaverPng()
        {
            Format = "png";
        }

        public void Save(string path, string name, Bitmap bitmap)
        {
            bitmap.Save(path + "\\" + name + "." + Format, ImageFormat.Png);
        }
    }
}
