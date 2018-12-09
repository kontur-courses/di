using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud
{
    public class Picture : IGraphics
    {
        private readonly Size imageSize;
        private readonly FontFamily fontFamily;
        private readonly Color wordColor;
        private readonly ImageFormat imageFormat;
        private readonly string imageName;

        public Picture(Size imageSize, FontFamily fontFamily, Color wordColor, ImageFormat imageFormat, string imageName)
        {
            this.imageSize = imageSize;
            this.fontFamily = fontFamily;
            this.wordColor = wordColor;
            this.imageFormat = imageFormat;
            this.imageName = imageName;
        }

        public void Save(IReadOnlyCollection<Tag> words)
        {
            using (var map = new Bitmap(imageSize.Width, imageSize.Height))
            using (var graphics = Graphics.FromImage(map))
            {
                var image = new PictureCreator(fontFamily, wordColor, words, graphics);
                image.DrawPicture();
                map.Save($"{imageName}",imageFormat);
            }
        }
    }
}