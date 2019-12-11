using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class AppVisualizer : IVisualizer
    {
        private readonly IReader reader;
        private readonly IExtractor extracter;
        private readonly ILayouter layouter;
        private readonly ParserList parserList;
        private readonly FilterList filterList;
        private readonly FontSettings fontSettings;
        private readonly ImageSettings imageSettings;
        private readonly ImageHolder imageHolder;
        private readonly ThemeList themeList;

        public AppVisualizer(IReader reader, IExtractor extracter, ILayouter layouter,
            ParserList parserList, FilterList filterList, FontSettings fontSettings, ThemeList themeList,
            ImageSettings imageSettings, ImageHolder imageHolder)
        {
            this.reader = reader;
            this.extracter = extracter;
            this.layouter = layouter;
            this.parserList = parserList;
            this.filterList = filterList;
            this.fontSettings = fontSettings;
            this.imageSettings = imageSettings;
            this.themeList = themeList;
            this.imageHolder = imageHolder;
        }

        public void Visualize()
        {
            var text = reader.ReadAllText("input.txt");
            var words = extracter.ExtractWords(text);
            var filteredWords = words;
            foreach (var filter in filterList.SelectedItems)
                filteredWords = filter.FilterWords(filteredWords).ToArray();
            var parsedWords = filteredWords;
            foreach (var parser in parserList.SelectedItems)
                parsedWords = parser.ParseWords(parsedWords).ToArray();
            var wordTokens = WordTokenizer.TokenizeWithNoSpeechPart(parsedWords);
            var theme = themeList.SelectedItems.First();
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(theme.BackgroundColor), 0, 0, imageSettings.Width, imageSettings.Height);
                foreach (var wordToken in wordTokens)
                    DrawWord(wordToken, graphics, theme);
            }
            imageHolder.UpdateUi();
            layouter.Reset();
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
