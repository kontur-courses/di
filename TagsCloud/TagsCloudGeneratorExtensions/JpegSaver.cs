using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGeneratorExtensions
{
    [Factorial("JpegSaver")]
    public class JpegSaver : ISaver
    {
        public bool TrySaveTo(string filePath, Bitmap bitmap)
        {
            if (filePath == null || bitmap == null)
                throw new ArgumentNullException();
            filePath = filePath.EndsWith(".jpeg") ? filePath : filePath + ".jpeg";
            try
            {
                bitmap.Save(filePath, ImageFormat.Jpeg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}