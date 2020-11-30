using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Settings;

namespace TagCloud.Infrastructure.Graphics
{
    class WordPainter : IPainter<string>
    {
        private readonly ILayouter<Size, Rectangle> layouter;
        private readonly Func<IFontSettingProvider> fontProvider;
        private readonly Func<IImageSettingsProvider> imageSettingsProvider;

        public WordPainter(
            ILayouter<Size, Rectangle> layouter, 
            Func<IFontSettingProvider> fontProvider, 
            Func<IImageSettingsProvider> imageSettingsProvider)
        {
            this.layouter = layouter;
            this.fontProvider = fontProvider;
            this.imageSettingsProvider = imageSettingsProvider;
        }
        
        public Image GetImage(Dictionary<string, Size> sizedTokens, Dictionary<string, int> tokenFontSizes)
        {
            var settings = imageSettingsProvider(); 
            var image = new Bitmap(settings.Width, settings.Height);
            var imageGraphics = System.Drawing.Graphics.FromImage(image);
            foreach (var word in sizedTokens.Keys)
            {
                var hitbox = layouter.GetPlace(sizedTokens[word]);

                var font = new Font(settings.FontFamily, tokenFontSizes[word]);
                imageGraphics.DrawString(word, font, settings.Brush, hitbox.Location);
            }
            return image;
        }
        
    }
}