namespace TagsCloudGenerator.Interfaces
{
    public interface IPainterAndSaver
    {
        bool TryPaintAndSave(
            (string word, System.Drawing.Font font, System.Drawing.RectangleF wordRectangle)[] layoutedWords,
            System.Drawing.Size imageSize,
            string pathToSave);
    }
}