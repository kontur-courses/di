using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public class TagCloud
    {
        public readonly Bitmap Image;

        public TagCloud(Bitmap image)
        {
            Image = image;
        }

        public bool TrySaveTo(string path, ImageFormat format)
        {
            try
            {
                Image.Save(path, format);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}