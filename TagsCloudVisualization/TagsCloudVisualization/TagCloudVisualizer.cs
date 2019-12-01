using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizer
    {
        private readonly ILayouter layouter;
        private readonly ImageSettings imageSettings;
        private readonly IParser textParser;

        public TagCloudVisualizer(IParser textParser, ILayouter layouter, ImageSettings imageSettings)
        {
            this.textParser = textParser;
            this.layouter = layouter;
            this.imageSettings = imageSettings;
        }

        public Bitmap VisualizeTextFromFile(string fileName)
        {
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var wordTokens = textParser.ParseToTokens(text);

            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var graphics = Graphics.FromImage(bmp);

            foreach (var word in wordTokens)
                DrawWord(graphics, word);

            return bmp;
        }

        private static Size CalculateWordSize(WordToken wordToken, Font font, Graphics graphics)
        {
            var size = graphics.MeasureString(wordToken.Tag, font);
            return size.ToSize();
        }

        private void DrawWord(Graphics graphics, WordToken word)
        {
            var wordFont = new Font(
                imageSettings.Font.FontFamily,
                CalculateWordSize(word),
                imageSettings.Font.Style
                );

            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(word, wordFont, graphics));
            graphics.DrawRectangle(new Pen(Color.Salmon), wordRectangle);
            graphics.DrawString(word.Tag, wordFont, new SolidBrush(imageSettings.FontColor), wordRectangle.Location);
        }

        private float CalculateWordSize(WordToken word)
        {
            return imageSettings.Font.Size + word.TextCount * 3;
        }
    }
}
                                                                                                                                 