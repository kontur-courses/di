namespace TagsCloudGenerator.Interfaces
{
    public interface IPainter : IFactorial
    {
        void DrawWords(
            (string word, float maxFontSymbolWidth, string fontName, System.Drawing.RectangleF wordRectangle)[] layoutedWords,
            System.Drawing.Graphics graphics);
    }
}