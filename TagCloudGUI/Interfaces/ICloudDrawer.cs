namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface ICloudDrawer
    {
        void DrawCloudFromPalette(IEnumerable<TextRectangle> rectangles, Point offsetPoint, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette);
        void DrawCloudRandomColor(IEnumerable<TextRectangle> rectangles, Point offsetPoint,
            IImageSettingsProvider drawImageSettingsProvider);
    }
}
