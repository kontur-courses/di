using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloud.ImageProcessing.Config;

namespace TagsCloud.ImageProcessing.SaverImage
{

    public class ImageSaver : IImageSaver
    {
        private readonly Dictionary<string, ImageFormat> formats;

        public ImageSaver()
        {
            formats = new Dictionary<string, ImageFormat>()
            {
                { ".png", ImageFormat.Png},
                { ".jpg", ImageFormat.Jpeg},
                { ".bmp", ImageFormat.Bmp}
            };
        }

        public void SaveImageWithConfig(Bitmap bitmap, IImageConfig imageConfig)
        {
            var path = imageConfig.Path;
            var format = formats.FirstOrDefault(pair => path.EndsWith(pair.Key)).Value;
            bitmap.Save(path, format);
        }
    }
}
