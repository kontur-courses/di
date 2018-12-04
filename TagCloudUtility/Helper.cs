using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using TagCloud.Utility.Models.Tag;

namespace TagCloud.Utility
{
    public static class Helper
    {
        public static Bitmap ResizeImage(Image image, string size)
        {
            var width = int.Parse(size.Split('x').First());
            var height = int.Parse(size.Split('x').Last());
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static TagContainer ReadTagsContainer(string text)
        {
            var tagGroups = new TagContainer();
            var groups = text
                .Split(';')
                .Where(line => !string.IsNullOrEmpty(line));

            foreach (var group in groups)
            {
                var items = group.Split(' ');
                var name = items[0];
                var freq = items[1]
                    .Split('-')
                    .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                    .ToArray();
                var size = items[2]
                    .Split('x')
                    .Select(int.Parse)
                    .ToArray();
                tagGroups.Add(name, new FrequencyGroup(freq[0], freq[1]), new Size(size[0], size[1]));
            }

            return tagGroups;
        }

        public static string GetPath(string path)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), path);
        }
    }
}