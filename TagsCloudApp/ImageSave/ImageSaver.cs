using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace TagsCloudApp.ImageSave
{
    public class Saver : IImageSaver
    {
        public void SaveImage(Bitmap bitmap, string directoryName, string filename)
        {            
            bitmap.Save(filename);
        }
    }
}
