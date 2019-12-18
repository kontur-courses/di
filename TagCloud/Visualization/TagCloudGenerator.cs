using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly PictureConfig pictureConfig;
        private readonly ITagCloudElementsPreparer tagCloudElementsPreparer;
        private readonly ITagCloudElementDrawer tagCloudElementDrawer;

        public TagCloudGenerator(
            PictureConfig pictureConfig,
            ITagCloudElementsPreparer tagCloudElementsPreparer,
            ITagCloudElementDrawer tagCloudElementDrawer)
        {
            this.pictureConfig = pictureConfig;
            this.tagCloudElementsPreparer = tagCloudElementsPreparer;
            this.tagCloudElementDrawer = tagCloudElementDrawer;
        }

        public Bitmap GetTagCloudBitmap(IEnumerable<Word> words)
        {
            var tagCloudElements = tagCloudElementsPreparer.PrepareTagCloudElements(words);
            var bitmap = new Bitmap(pictureConfig.Size.Width, pictureConfig.Size.Height);
            var g = Graphics.FromImage(bitmap);
            g.Clear(pictureConfig.Palette.BackgroundColor);
            foreach (var element in tagCloudElements)
            {
                tagCloudElementDrawer.Draw(g, element);
            }
            return bitmap;
        }

    }
}
