using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace TagsCloudContainer.ImageSaver
{
    class Saver : IImageSaver
    {
        public void SaveImage(Bitmap bitmap, string directoryName, string filename)
        {
            var directoryInfo = DirectoryMethods.GetProjectDirectoryInfo();
            Directory.CreateDirectory(directoryInfo.FullName + @"\" + directoryName);
            bitmap.Save(directoryInfo.FullName + $@"\{directoryName}\{filename}");
        }
    }
}
