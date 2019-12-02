namespace TagsCloudGenerator.Interfaces
{
    public interface IPainter
    {
        void DrawWords(
            (string word, System.Drawing.Font font, System.Drawing.RectangleF wordRectangle)[] layoutedWords,
            System.Drawing.Graphics graphics);
    }
}