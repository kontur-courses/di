using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;

namespace TagsCloud.ImageProcessing.Config.ImageBuilders
{
    public class ImageBuilder : IImageBuilder
    {
        private readonly ILayouter layouter;
        private readonly IImageConfig imageConfig;
        private readonly TextOperator textOperator;
        private readonly IWordsConfig wordsConfig;

        public ImageBuilder(ILayouter layouter, IImageConfig imageConfig, IWordsConfig wordsConfig, TextOperator textOperator)
        {
            this.layouter = layouter;
            this.imageConfig = imageConfig;
            this.textOperator = textOperator;
            this.wordsConfig = wordsConfig;
        }

        public Bitmap BuildImage(string textPath)
        {
            var imageSize = imageConfig.ImageSize;
            layouter.SetCenter(new Point(imageSize.Width / 2, imageSize.Height / 2));

            var words = textOperator.ReadFromFile(textPath).OrderByDescending(info => info.Count).ToList();

            var image = new Bitmap(imageConfig.ImageSize.Width, imageConfig.ImageSize.Height);
            using var graphics = Graphics.FromImage(image);
            using var brush = new SolidBrush(wordsConfig.Color);

            /// Пока не определил как можно по заданному размеру изображения 
            /// Определить размер шрифта в зависимости от частоты слова
            var count = 40;
            foreach (var word in words.Take(40))
            {
                var currentFont = new Font(wordsConfig.FontName.FontFamily, count);
                var size = TextRenderer.MeasureText(word.Value, currentFont);
                graphics.DrawString(word.Value, currentFont, brush, layouter.PutNextRectangle(size));
                count--;
            }
            return image;
        }
    }
}
