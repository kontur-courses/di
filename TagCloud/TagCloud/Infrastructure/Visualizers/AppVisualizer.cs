using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class AppVisualizer : IVisualizer
    {
        private readonly Reader reader;
        private readonly IExtractor extracter;
        private readonly ILayouter layouter;
        private readonly IParser[] parsers;
        private readonly IFilter[] filters;
        private readonly FontSettings fontSettings;
        private readonly ImageSettings imageSettings;
        private readonly ImageHolder imageHolder;
        private readonly ITheme[] themes;

        public AppVisualizer(Reader reader, IExtractor extracter, ILayouter layouter,
            IParser[] parsers, IFilter[] filters, FontSettings fontSettings, ITheme[] themes,
            ImageSettings imageSettings, ImageHolder imageHolder)
        {
            this.reader = reader;
            this.extracter = extracter;
            this.layouter = layouter;
            this.parsers = parsers;
            this.filters = filters;
            this.fontSettings = fontSettings;
            this.imageSettings = imageSettings;
            this.themes = themes;
            this.imageHolder = imageHolder;
        }

        public void Visualize()
        {
            var wordTokens = GetWordTokens();
            DrawWordTokens(wordTokens);
            imageHolder.UpdateUi();
            layouter.Reset();
        }

        private WordToken[] GetWordTokens()
        {
            var textReader = reader.TextReader;
            var text = textReader.ReadAllText(reader.PathToFile);
            var words = extracter.ExtractWords(text);
            var filteredWords = words;
            foreach (var filter in filters)
                if (filter.IsChecked)
                    filteredWords = filter.FilterWords(filteredWords).ToArray();
            var parsedWords = filteredWords;
            foreach (var parser in parsers)
                if (parser.IsChecked)
                    parsedWords = parser.ParseWords(parsedWords).ToArray();
            return WordTokenizer.TokenizeWithNoSpeechPart(parsedWords);
        }

        private void DrawWordTokens(WordToken[] wordTokens)
        {
            var theme = themes.Where(t => t.IsChecked).First();
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(theme.BackgroundColor), 0, 0, imageSettings.Width, imageSettings.Height);
                foreach (var wordToken in wordTokens)
                    DrawWord(wordToken, graphics, theme);
            }
        }

        private void DrawWord(WordToken wordToken, Graphics graphics, ITheme theme)
        {
            var font = new Font(fontSettings.FontFamily, GetFontSize(wordToken), fontSettings.Style);
            var wordRectangle = layouter.PutNextRectangle(GetWordSize(wordToken, graphics, font));
            graphics.DrawString(wordToken.Value, font, new SolidBrush(theme.GetWordFontColor(wordToken)), wordRectangle);
        }

        private float GetFontSize(WordToken wordToken) =>
             fontSettings.DefaultSize + wordToken.Count * fontSettings.CountMultiplier;
        private SizeF GetWordSize(WordToken wordToken, Graphics graphics, Font font) =>
            graphics.MeasureString(wordToken.Value, font);
    }
}
