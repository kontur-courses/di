using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudForm.Actions
{
    public class CloudWithWordsPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly CircularCloudLayouterSettings settings;
        private readonly Palette palette;
        private Size imageSize;
        private ICircularCloudLayouter layouter;
        private Dictionary<string, int> words;

        public CloudWithWordsPainter(IImageHolder imageHolder,
            CircularCloudLayouterSettings settings, Palette palette, ICircularCloudLayouter layouter,
            Dictionary<string, int> words)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.layouter = layouter;
            this.words = words;
            imageSize = imageHolder.GetImageSize();
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width,
                    imageSize.Height);
                var backgroundBrush = new SolidBrush(palette.SecondaryColor);
                var rectBrush = new Pen(palette.PrimaryColor);
                foreach (var word in words)
                {
                    var font = new Font("Arial", word.Value * 5);
                    var size = new Size(TextRenderer.MeasureText(word.Key, font).Width, TextRenderer.MeasureText(word.Key, font).Height);
                    var rect = layouter.PutNextRectangle(size);
                    graphics.FillRectangle(backgroundBrush, rect);
                    graphics.DrawRectangle(rectBrush, rect);

                    graphics.DrawString(word.Key, font, new SolidBrush(palette.PrimaryColor), new PointF(rect.X, rect.Y));
                }
            }

            imageHolder.UpdateUi();
        }
    }
}
