using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudApplication;

namespace TagCloudApplicationTest.Savers
{
    public class SimpleBMPSaver : ISaver
    {
        public void Save(string fileName, Bitmap image)
        {
            var fullFileName = AppContext.BaseDirectory + $"{fileName}.bmp";
            image.Save(fullFileName, ImageFormat.Bmp);
        }
    }
}
