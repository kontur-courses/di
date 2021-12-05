using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Writers
{
    public class BitmapWriter : IFileWriter
    {
        public void Write(Bitmap bitmap, string filename, ImageFormat format)
        {
            var env = Environment.CurrentDirectory + "\\";
            bitmap.Save(env + filename, format);
        }
    }
}
