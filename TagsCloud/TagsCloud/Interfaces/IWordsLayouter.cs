namespace TagsCloudGenerator.Interfaces
{
    public interface IWordsLayouter
    {
        (string word, System.Drawing.Font font, System.Drawing.RectangleF wordRectangle)[] ArrangeWords(string[] words, string font);
    }
}