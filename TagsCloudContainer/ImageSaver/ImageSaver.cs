using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace TagsCloudContainer.ImageSaver
{
    class ImageSaver : IImageSaver
    {
        public void SaveImage(Image image, string path)
        {
            image.Save(path);
        }
    }
}
