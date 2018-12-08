using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.Savers
{
    public class BMPSaver : ISaver
    {
        public void Save(string fileName, Bitmap image)
        {
            image.Save(fileName, ImageFormat.Bmp);
        }
    }
}
