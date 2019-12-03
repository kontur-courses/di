using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly CircularCloudLayouterSettings settings;
        private readonly Palette palette;
        private Size imageSize;
        private ICircularCloudLayouter layouter;

        public CloudPainter(IImageHolder imageHolder,
            CircularCloudLayouterSettings settings, Palette palette, ICircularCloudLayouter layouter)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.layouter = layouter;
            imageSize = imageHolder.GetImageSize();
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width, imageSize.Height);
                var rnd = new Random();
                var backgroundBrush = new SolidBrush(palette.SecondaryColor);
                var rectBrush = new Pen(palette.PrimaryColor);
                for (int i = 0; i < settings.IterationsCount; i++)
                {
                    var size = new Size(rnd.Next(settings.MinSize, settings.MaxSize),
                        rnd.Next(settings.MinSize, settings.MaxSize));
                    var rect = layouter.PutNextRectangle(size);
                    graphics.FillRectangle(backgroundBrush, rect);
                    graphics.DrawRectangle(rectBrush, rect);
                }
            }
            imageHolder.UpdateUi();
        }
    }
}
