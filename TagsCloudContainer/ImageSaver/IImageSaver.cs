using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.ImageSaver
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string path);
    }
}
