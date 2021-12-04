using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Writers
{
    public class BitmapWriter : IFileWriter
    {
        public void Write(Bitmap bitmap, string filename, ImageFormat format)
        {
            var env = Environment.CurrentDirectory + "\\";
            bitmap.Save(env + "out3.png", format);
        }
    }
}
