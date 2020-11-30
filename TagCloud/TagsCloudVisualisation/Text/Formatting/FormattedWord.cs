using System.Drawing;

namespace TagsCloudVisualisation.Text.Formatting
{
    public class FormattedWord
    {
        public FormattedWord(string word, Font font, Brush brush)
        {
            Word = word;
            Font = font;
            Brush = brush;
        }

        public string Word { get; }
        public Font Font { get; }
        public Brush Brush { get; }

        public static FormattedWord MultiplyFontSize(FormattedWord formattedWord, int coefficient) =>
            new FormattedWord(word: formattedWord.Word, brush: formattedWord.Brush,
                font: new Font(
                    formattedWord.Font.FontFamily,
                    formattedWord.Font.Size * coefficient,
                    formattedWord.Font.Style,
                    formattedWord.Font.Unit,
                    formattedWord.Font.GdiCharSet,
                    formattedWord.Font.GdiVerticalFont));
    }
}