namespace TagsCloudGenerator.Interfaces
{
    public interface IWordsLayouter : IFactorial
    {
        (string word, float maxFontSymbolWidth, string fontName, System.Drawing.RectangleF wordRectangle)[] ArrangeWords(
            string[] words);
    }
}