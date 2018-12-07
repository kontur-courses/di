using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class TagsCloudVisualizer : IVisualizer
    {
        private IEnumerable<IWord> Words { get; set; }
        private IConfiguration Configuration { get; set; }
        private ICloudLayouter Layouter { get; set; }

        public TagsCloudVisualizer(IWordsGenerator wordsGenerator, IConfiguration configuration, ICloudLayouter layouter)
        {
            Words = wordsGenerator.GenerateWords();
            Configuration = configuration;
            Layouter = layouter;
        }

        public void Visualize()
        {
            var folder = new DirectoryInfo(Configuration.DirectoryToSave);
            var filename = Configuration.OutFileName;

            var color = Color.FromName(Configuration.Color);
            var brush = new SolidBrush(color);

            var wordsOrderedByFontSize = Words.OrderByDescending(word => word.Font.Size);

            using (var bmp = new Bitmap(Configuration.ImageWidth, Configuration.ImageHeight))
            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                foreach (var word in wordsOrderedByFontSize)
                {
                    var wordSize = g.MeasureString(word.Value, word.Font);
                    var wordPosition = Layouter.PutNextRectangleF(wordSize);

                    g.DrawString(word.Value, word.Font, brush, wordPosition);
                }

                bmp.Save(Path.Combine(folder.FullName, $"{filename}.png"), ImageFormat.Png);
            }
        }
    }
}