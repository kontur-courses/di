using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.DefinerFontSize;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Algorithm;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Painters
{
    public class CloudPainter
    {
        private readonly IAnalyzer analyzer;
        private readonly IDefinerFontSize definerFont;
        private readonly IImageHolder imageHolder;
        private readonly IPaletteSettings paletteSettings;
        private readonly IWordsProvider wordsProvider;

        public CloudPainter(IImageHolder imageHolder,
            IWordsProvider wordsProvider,
            IAnalyzer analyzer, IDefinerFontSize definerFont,
            IPaletteSettings paletteSettings)
        {
            this.wordsProvider = wordsProvider;
            this.imageHolder = imageHolder;
            this.definerFont = definerFont;
            this.analyzer = analyzer;
            this.paletteSettings = paletteSettings;
        }

        public void Paint(string textFilePath, ICurve curve)
        {
            var imageSize = imageHolder.GetImageSize();
            var cloud = new Cloud(curve);
            var words = wordsProvider.GetWords(textFilePath);
            var analyzedWords = analyzer.CreateWordFrequenciesSequence(words);
            var wordsWithFont = definerFont.DefineFontSize(analyzedWords);
            var counter = 0;
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(paletteSettings.BackgroundColor), 0, 0,
                    imageSize.Width, imageSize.Height);

                foreach (var word in wordsWithFont.OrderBy(w => w.Font.Size).Reverse())
                {
                    var color = paletteSettings.GetColorAccordingSize(word.Font.Size);
                    using var brush = new SolidBrush(color);
                    var sizeRectangle = TextRenderer.MeasureText(word.Word, word.Font);
                    sizeRectangle.Width++;
                    sizeRectangle.Height++;
                    var r = cloud.PutNextRectangle(sizeRectangle);
                    var drawFormat = new StringFormat { Alignment = StringAlignment.Center };
                    graphics.DrawString(word.Word, word.Font, brush, r, drawFormat);
                    if (++counter % 10 == 0)
                        imageHolder.UpdateUi();
                }
            }

            imageHolder.UpdateUi();
        }
    }
}