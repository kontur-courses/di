using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudVisualizer;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class ImageGenerator : IImageGenerator
    {
        private readonly ImageSettings settings;

        public ImageGenerator(ImageSettings settings)
        {
            this.settings = settings;
        }

        public Bitmap GenerateImage(IEnumerable<Tag> cloud)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using var g = Graphics.FromImage(image);
            using var brush = new SolidBrush(Color.Black);
            foreach (var tag in cloud)
                g.DrawString(tag.Word, new Font(settings.FontName, (float) tag.FontSize), brush, tag.Location);

            return image;
        }
    }
}