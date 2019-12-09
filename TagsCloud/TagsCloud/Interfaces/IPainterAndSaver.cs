namespace TagsCloudGenerator.Interfaces
{
    public interface IPainterAndSaver
    {
        bool TryPaintAndSave(
            (string word, float maxFontSymbolWidth, string fontName, System.Drawing.RectangleF wordRectangle)[] layoutedWords,
            string pathToSave);
    }
}