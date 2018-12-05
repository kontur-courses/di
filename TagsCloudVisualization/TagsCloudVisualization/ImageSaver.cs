using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public static class ImageSaver
    {
        public static void WriteToFile(string fileName, Image bitmap)
        {
            bitmap.Save(fileName, bitmap.RawFormat);
        }
    }
}
