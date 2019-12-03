using System.Drawing;

namespace TagCloud
{
    public class DefaultVisualizer : IVisualizer
    {
        private readonly IReader reader;
        private readonly IExtracter extracter;
        private readonly ILayouter layouter;
        private readonly FontSettings fontSettings;
        private readonly ImageSettings imageSettings;

        public DefaultVisualizer(IReader reader, IExtracter extracter, ILayouter layouter,
            FontSettings fontSettings, ImageSettings imageSettings, LayouterSettings layouterSettings)
        {
            this.reader = reader;
            this.extracter = extracter;
            this.layouter = layouter;
            this.fontSettings = fontSettings;
            this.imageSettings = imageSettings;
        }

        public Bitmap VisualizeTextFromFile(string path)
        {
            var text = reader.ReadAllText(path);
            var wordTokens = extracter.ExtractWordTokens(text);
            var image = new Bitmap(imageSettings.Width, imageSettings.Height);
            var graphics = Graphics.FromImage(image);
            foreach (var wordToken in wordTokens)
                DrawWord(wordToken, graphics);
            image.Save("image.bmp");
            return image;
        }

        private void DrawWord(WordToken wordToken, Graphics graphics)
        {
            var font = new Font(fontSettings.Family, GetFontSize(wordToken), fontSettings.Style);
            var wordRectangle = layouter.PutNextRectangle(GetWordSize(wordToken, graphics, font));
            graphics.DrawString(wordToken.Value, font, new SolidBrush(fontSettings.Color), wordRectangle);
        }

        private float GetFontSize(WordToken wordToken) =>
             fontSettings.DefaultSize + wordToken.Count * fontSettings.CountMultiplier;
        private SizeF GetWordSize(WordToken wordToken, Graphics graphics, Font font) =>
            graphics.MeasureString(wordToken.Value, font);
    }
}
