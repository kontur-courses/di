using System.Drawing;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public readonly struct WordToDraw
    {
        public WordToDraw(string word, Font font, Brush brush)
        {
            Word = word;
            Font = font;
            Brush = brush;
        }

        public string Word { get; }
        public Font Font { get; }
        public Brush Brush { get; }

        public static Font MultiplyFontSize(Font fontPrototype, int coefficient) =>
            new Font(fontPrototype.FontFamily, fontPrototype.Size * coefficient, fontPrototype.Style,
                fontPrototype.Unit, fontPrototype.GdiCharSet, fontPrototype.GdiVerticalFont);
    }
}