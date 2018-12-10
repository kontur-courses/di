using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.ImageCreators
{
    public class ImageCreator
    {
        private readonly ImageSettings imageSettings;
        private readonly TextSettings textSettings;
        private readonly Palette palette;
        private readonly Func<ICircularCloudLayouter> circularCloudLayouterFactory;


        public ImageCreator(ImageSettings imageSettings, TextSettings textSettings, Func<ICircularCloudLayouter> circularCloudLayouterFactory, Palette palette)
        {
            this.imageSettings = imageSettings;
            this.textSettings = textSettings;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.palette = palette;
        }

        public Image GetImage(IEnumerable<WordInfo> wordInfos)
        {
            var bitmap = GetBitmap();
            var labels = GetDrawingLabels(wordInfos);
            DrawLabelsOnBitmap(bitmap, labels);
            return bitmap;
        }

        private void DrawLabelsOnBitmap(Bitmap bitmap, IEnumerable<Label> labels)
        {
            var circularCloudLayouter = circularCloudLayouterFactory.Invoke();
            foreach (var label in labels)
            {
                var rectangle = circularCloudLayouter.PutNextRectangle(label.Size);
                label.DrawToBitmap(bitmap, rectangle);
            }
        }

        private IEnumerable<Label> GetDrawingLabels(IEnumerable<WordInfo> wordInfos)
        {
            var words = textSettings.FontSizeChooser.GetWordInfos(wordInfos);
            var bitmap = GetBitmap();
            var graphics = Graphics.FromImage(bitmap);

            foreach (var printedWordInfo in words)
            {
                var font = new Font(textSettings.Family, printedWordInfo.FontSize);
                var sizeText = graphics.MeasureString(printedWordInfo.Word, font).ToSize();
                var sizeLabel = new Size(sizeText.Width + 20, sizeText.Height + 20);
                var label = new Label
                {
                    Text = printedWordInfo.Word, ForeColor = textSettings.ColorChooser.GetNextColor(), Font = font, Size = sizeLabel, BackColor = palette.BackGroundColor
                };
                label.Update();
                yield return label;
            }
        }

        private Bitmap GetBitmap()
        {
            var size = imageSettings.ImageSize;
            var bitmap = new Bitmap(size.Width, size.Height);
            using (var graphics = Graphics.FromImage(bitmap))
                graphics.Clear(palette.BackGroundColor);
            return bitmap;
        }
    }
}