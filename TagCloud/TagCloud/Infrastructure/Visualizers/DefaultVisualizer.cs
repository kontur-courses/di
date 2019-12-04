using System.Drawing;

namespace TagCloud
{
    public class DefaultVisualizer : IVisualizer
    {
        private readonly IReader reader;
        private readonly IExtractor extracter;
        private readonly ILayouter layouter;
        private readonly IParser parser;
        private readonly IFilter filter;
        private readonly FontSettings fontSettings;
        private readonly ImageSettings imageSettings;
        private readonly FileSettings fileSettings;

        public DefaultVisualizer(IReader reader, IExtractor extracter, ILayouter layouter,
            IParser parser, IFilter filter, FontSettings fontSettings,
            ImageSettings imageSettings, FileSettings fileSettings)
        {
            this.reader = reader;
            this.extracter = extracter;
            this.layouter = layouter;
            this.parser = parser;
            this.filter = filter;
            this.fontSettings = fontSettings;
            this.imageSettings = imageSettings;
            this.fileSettings = fileSettings;
        }

        public Bitmap VisualizeTextFromFile(string path)
        {
            var text = reader.ReadAllText(path);
            var words = extracter.ExtractWords(text);
            var filteredWords = filter.FilterWords(words);
            var parsedWords = parser.ParseWords(filteredWords);
            var wordTokens = WordTokenizer.Tokenize(parsedWords);
            var image = new Bitmap(imageSettings.Width, imageSettings.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (var wordToken in wordTokens)
                    DrawWord(wordToken, graphics);
            }
            image.Save($"{fileSettings.ImageSavePath}\\TagCloud.{fileSettings.ImageFormat}");
            return image;
        }

        private void DrawWord(WordToken wordToken, Graphics graphics)
        {
            var font = new Font(fontSettings.FontFamily, GetFontSize(wordToken), fontSettings.Style);
            var wordRectangle = layouter.PutNextRectangle(GetWordSize(wordToken, graphics, font));
            graphics.DrawString(wordToken.Value, font, new SolidBrush(fontSettings.Color), wordRectangle);
        }

        private float GetFontSize(WordToken wordToken) =>
             fontSettings.DefaultSize + wordToken.Count * fontSettings.CountMultiplier;
        private SizeF GetWordSize(WordToken wordToken, Graphics graphics, Font font) =>
            graphics.MeasureString(wordToken.Value, font);
    }
}
