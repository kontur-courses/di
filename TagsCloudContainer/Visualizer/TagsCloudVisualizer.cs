using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.Tag;

namespace TagsCloudContainer.Visualizer
{
    public class TagsCloudVisualizer : IVisualizer
    {
        private IConfiguration Configuration { get; }
        private ICloudLayouter Layouter { get; }

        public TagsCloudVisualizer(IConfiguration configuration, ICloudLayouter layouter)
        {
            Configuration = configuration;
            Layouter = layouter;
        }

        public byte[] Visualize(IEnumerable<ITag> tags)
        {
            var color = Color.FromName(Configuration.Color);
            var brush = new SolidBrush(color);

            using (var bmp = new Bitmap(Configuration.ImageWidth, Configuration.ImageHeight))
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