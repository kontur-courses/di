using System;
using System.Drawing;
using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Common.Settings;
using CloudLayouter.Infrastructer.Interfaces;

namespace CloudLayouter.Painters
{
    public class TagCloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;
        private readonly Palette palette;
        private readonly TagSettings settings;

        public TagCloudPainter(IImageHolder imageHolder,
            TagSettings settings, Palette palette, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.imageSettings = imageSettings;
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor),
                    new Rectangle(new Point(0, 0), new Size(imageSettings.Width, imageSettings.Height)));
                var random = new Random();
                var cloudLayouter =
                    new CircularCloudLayouter(new Point(imageSettings.Width / 2, imageSettings.Height / 2));
                for (var i = 0; i < settings.CountOfTags; i++)
                    cloudLayouter.PutNextRectangle(new Size(random.Next(settings.MinWidth, settings.MaxWidth),
                        random.Next(settings.MinHeight, settings.MaxHeight)));
                cloudLayouter.Draw(graphics);
            }

            imageHolder.UpdateUi();
        }
    }
}