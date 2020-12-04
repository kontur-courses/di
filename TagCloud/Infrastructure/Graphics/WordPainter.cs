using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    class WordPainter : IPainter<string>
    {
        private readonly ILayouter<Size, Rectangle> layouter;
        private readonly Func<IImageSettingsProvider> imageSettingsProvider;

        public WordPainter(
            ILayouter<Size, Rectangle> layouter, 
            Func<IImageSettingsProvider> imageSettingsProvider)
        {
            this.layouter = layouter;
            this.imageSettingsProvider = imageSettingsProvider;
        }
        
        public Image GetImage(IEnumerable<(string, TokenInfo)> tokens)
        {
            var settings = imageSettingsProvider(); 
            using var image = new Bitmap(settings.Width, settings.Height);
            using var imageGraphics = System.Drawing.Graphics.FromImage(image);
            foreach (var (word, info) in tokens)
            {
                var hitbox = layouter.GetPlace(info.Size);

                var font = new Font(settings.FontFamily, info.FontSize);
                imageGraphics.DrawString(word, font, settings.Brush, hitbox.Location);
            }
            return image;
        }
    }
}