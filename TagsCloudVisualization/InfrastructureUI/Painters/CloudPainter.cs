using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Algorithm;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Painters
{
    public class CloudPainter
    {
        private readonly IAnalyzer analyzer;
        private readonly ICurve curve;
        private readonly DefinerSize definer;
        private readonly IImageHolder imageHolder;
        private readonly PaletteSettings paletteSettings;

        public CloudPainter(IImageHolder imageHolder,
            IAnalyzer analyzer, DefinerSize definer,
            ICurve curve
            , PaletteSettings paletteSettings)
        {
            this.imageHolder = imageHolder;
            this.definer = definer;
            this.analyzer = analyzer;
            this.curve = curve;
            this.paletteSettings = paletteSettings;
        }

        public void Paint()
        {
            var imageSize = imageHolder.GetImageSize();
            var cloud = new Cloud(curve);
            var frequency = analyzer.CreateFrequencyDictionary();
            var words = definer.DefineFontSize(frequency);
            var counter = 0;
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(paletteSettings.BackgroundColor), 0, 0,
                    imageSize.Width, imageSize.Height);

                foreach (var word in words.Keys.OrderBy(w => frequency[w]).Reverse())
                {
                    var color = paletteSettings.GetColorAccordingSize(words[word].Size);
                    using var brush = new SolidBrush(color);
                    var sizeRectangle = TextRenderer.MeasureText(word, words[word]);
                    sizeRectangle.Width++;
                    sizeRectangle.Height++;
                    var r = cloud.PutNextRectangle(sizeRectangle);
                    var drawFormat = new StringFormat { Alignment = StringAlignment.Center };
                    graphics.DrawString(word, words[word], brush, r, drawFormat);
                    if (++counter % 10 == 0)
                        imageHolder.UpdateUi();
                }
            }

            imageHolder.UpdateUi();
        }
    }
}