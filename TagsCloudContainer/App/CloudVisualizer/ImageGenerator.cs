using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class ImageGenerator : IImageGenerator
    {
        public Bitmap GenerateImage(Dictionary<string, Rectangle> cloud, Size imageSize)
        {
            using var image = new Bitmap(imageSize.Width, imageSize.Height);
            using var g = Graphics.FromImage(image);
            using var brush = new SolidBrush(Color.Black);
            foreach (var pair in cloud)
            {
                var word = pair.Key;
                var rectangle = pair.Value;
                g.DrawString(word, Font.FromLogFont(10), brush, rectangle);
            }

            return image;
        }
    }
}