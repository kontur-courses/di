using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudVisualizer;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudPainter : ICloudPainter
    {
        private readonly AppSettings settings = AppSettings.Default;

        public void Paint(IEnumerable<Tag> cloud, Graphics g)
        {
            using var backgroundBush = new SolidBrush(settings.Palette.BackgroundColor);
            g.FillRectangle(backgroundBush, 0, 0, settings.ImageSettings.Width, settings.ImageSettings.Height);
            using var textBrush = new SolidBrush(settings.Palette.TextColor);
            foreach (var tag in cloud)
            {
                var font = settings.FontSettings.Font;
                g.DrawString(tag.Word, new Font(font.FontFamily,
                        (float) tag.FontSize, font.Style,
                        font.Unit, font.GdiCharSet),
                    textBrush, tag.Location);
            }
        }
    }
}