using System.Drawing;
using System.Linq;
using TagCloud.Algorithm;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly PictureConfig pictureConfig;
        private readonly IWordsProvider wordsProvider;
        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly IWordPainter wordPainter;

        public TagCloudGenerator(
            PictureConfig pictureConfig, 
            IWordsProvider wordsProvider, 
            ITagCloudLayouter tagCloudLayouter,
            IWordPainter wordPainter)
        {
            this.pictureConfig = pictureConfig;
            this.wordsProvider = wordsProvider;
            this.tagCloudLayouter = tagCloudLayouter;
            this.wordPainter = wordPainter;
        }

        public Bitmap GetTagCloudBitmap()
        {
            var words = wordsProvider.GetWords();
            var bitmap = new Bitmap(pictureConfig.Size.Width, pictureConfig.Size.Height);
            var g = Graphics.FromImage(bitmap);
            g.Clear(pictureConfig.Palette.BackgroundColor);

            foreach (var pair in words.Select((w, i) => new {Word = w, Index = i}))
            {
                var word = pair.Word;
                var index = pair.Index;
                var rectangle = tagCloudLayouter.PutNextRectangle(word.WordRectangleSize);
                var color = wordPainter.GetWordColor(word, index);
                g.DrawString(word.Value, 
                    new Font(pictureConfig.FontFamily, word.FontSize), 
                    new SolidBrush(color), 
                    rectangle);
            }

            return bitmap;
        }

    }
}
