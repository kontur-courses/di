using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Algorithm;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.Infrastructure.Analyzer;

namespace TagsCloudVisualization.InfrastructureUI.Painters
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly DefinerSize definer;
        private readonly IAnalyzer analyzer;
        private readonly ICurve curve;

        public CloudPainter(IImageHolder imageHolder, IAnalyzer analyzer, DefinerSize definer, ICurve curve)
        {
            this.imageHolder = imageHolder;
            this.definer = definer;
            this.analyzer = analyzer;
            this.curve = curve;

        }

        public void Paint()
        {
            var imageSize = imageHolder.GetImageSize();
            var cloud = new Cloud(curve);
            var frequency = analyzer.CreateFrequencyDictionary();
            var words = definer.DefineFontSize(frequency);
            var random = new Random();
            var brush = new SolidBrush(
                Color.FromArgb(random.Next(1, 255), random.Next(1, 255), random.Next(1, 255)));
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(Color.Wheat), 0, 0, imageSize.Width, imageSize.Height);
                var counter = 0;
                foreach (var word in words.Keys.OrderBy(w => frequency[w]).Reverse())
                {
                    var b = TextRenderer.MeasureText(word, words[word]);
                    var r = cloud.PutNextRectangle(new Size(b.Width + 1, b.Height + 1));
                    var r1 = new RectangleF(r.X, r.Y, r.Width + 1, r.Height + 1);
                    var drawFormat = new StringFormat { Alignment = StringAlignment.Center };
                    graphics.DrawString(word, words[word], brush, r1, drawFormat);
                    if(++counter % 100 == 0)
                        imageHolder.UpdateUi();
                }
            }
            imageHolder.UpdateUi();
        }
    }
}
