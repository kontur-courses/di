using TagsCloudVisualization;

namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface ICloudDrawer
    {
        void DrawCloud(IEnumerable<TextRectangle> rectangles, Point offsetPoint, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette);
    }
}
