using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Algorithm;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly PictureConfig pictureConfig;
        private readonly ITagCloudElementsPreparer tagCloudElementsPreparer;
        private readonly ITagCloudElementPainter tagCloudElementPainter;

        public TagCloudGenerator(
            PictureConfig pictureConfig,
            ITagCloudElementsPreparer tagCloudElementsPreparer,
            ITagCloudElementPainter tagCloudElementPainter)
        {
            this.pictureConfig = pictureConfig;
            this.tagCloudElementsPreparer = tagCloudElementsPreparer;
            this.tagCloudElementPainter = tagCloudElementPainter;
        }

        public Bitmap GetTagCloudBitmap(IEnumerable<Word> words)
        {
            var tagCloudElements = tagCloudElementsPreparer.PrepareTagCloudElements(words);
            var bitmap = new Bitmap(pictureConfig.Size.Width, pictureConfig.Size.Height);
            var g = Graphics.FromImage(bitmap);
            g.Clear(pictureConfig.Palette.BackgroundColor);
            foreach (var element in tagCloudElements)
            {
                tagCloudElementPainter.Paint(g, element);
            }
            return bitmap;
        }

    }
}
