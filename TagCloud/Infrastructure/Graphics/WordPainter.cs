using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public class WordPainter : IPainter<string>
    {
        private readonly ColorPicker colorPicker;
        private readonly Func<IImageSettingsProvider> imageSettingsProvider;
        private readonly ILayouter<Size, Rectangle> layouter;

        public WordPainter(
            ILayouter<Size, Rectangle> layouter,
            Func<IImageSettingsProvider> imageSettingsProvider,
            ColorPicker colorPicker)
        {
            this.layouter = layouter;
            this.imageSettingsProvider = imageSettingsProvider;
            this.colorPicker = colorPicker;
        }

        public Image GetImage(IEnumerable<(string, TokenInfo)> tokens)
        {
            var settings = imageSettingsProvider();
            var image = new Bitmap(settings.Width, settings.Height);
            using var imageGraphics = System.Drawing.Graphics.FromImage(image);

            using (layouter)
            {
                foreach (var (word, info) in tokens)
                {
                    var hitbox = layouter.GetPlace(info.Size);
                    using var font = new Font(settings.FontFamily, info.FontSize);
                    var brush = new SolidBrush(colorPicker.GetColor(info));
                    imageGraphics.DrawString(word, font, brush, hitbox.Location);
                }
            }

            return image;
        }
    }
}