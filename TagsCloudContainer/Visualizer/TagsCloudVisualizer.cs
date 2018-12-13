using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Tag;

namespace TagsCloudContainer.Visualizer
{
    public class TagsCloudVisualizer : IVisualizer
    {
        private IVisualizerSettings Settings { get; }
        private ICloudLayouter Layouter { get; }

        public TagsCloudVisualizer(IVisualizerSettings settings, ICloudLayouter layouter)
        {
            Settings = settings;
            Layouter = layouter;
        }

        public byte[] Visualize(IEnumerable<ITag> tags)
        {
            var color = Color.FromName(Settings.Color);
            var brush = new SolidBrush(color);

            using (var bmp = new Bitmap(Settings.ImageWidth, Settings.ImageHeight))
            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                foreach (var word in tags)
                {
                    var wordSize = g.MeasureString(word.Value, word.Font);
                    var wordPosition = Layouter.PutNextRectangleF(wordSize);

                    g.DrawString(word.Value, word.Font, brush, wordPosition);
                }

                return bmp.ToByteArray();
            }
        }
    }
}