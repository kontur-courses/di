using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Tag;

namespace TagsCloudContainer.Visualizer
{
    public class TagsCloudVisualizer : IVisualizer
    {
        private readonly IVisualizerSettings settings;
        private readonly ICloudLayouter layouter;

        public TagsCloudVisualizer(IVisualizerSettings settings, ICloudLayouter layouter)
        {
            this.settings = settings;
            this.layouter = layouter;
        }

        public byte[] Visualize(IEnumerable<ITag> tags)
        {
            var brush = new SolidBrush(settings.Color);

            using (var bmp = new Bitmap(settings.ImageWidth, settings.ImageHeight))
            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                foreach (var word in tags)
                {
                    var wordSize = g.MeasureString(word.Value, word.Font);
                    var wordPosition = layouter.PutNextRectangleF(wordSize);

                    g.DrawString(word.Value, word.Font, brush, wordPosition);
                }

                return bmp.ToByteArray(settings.ImageFormat);
            }
        }
    }
}