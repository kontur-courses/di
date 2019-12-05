using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.ImageSaver
{
    interface IImageSaver
    {
        void SaveImage(Bitmap bitmap, string directoryName, string filename);
    }
}
