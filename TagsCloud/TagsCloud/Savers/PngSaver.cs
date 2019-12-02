using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Savers
{
    public class PngSaver : ISaver
    {
        public bool TrySaveTo(string filePath, Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));
            filePath = filePath != null ?
                filePath.EndsWith(".png") ? filePath : filePath + ".png" :
                throw new ArgumentNullException(nameof(filePath));
            try
            {
                bitmap.Save(filePath, ImageFormat.Png);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}