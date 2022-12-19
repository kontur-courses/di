using TagCloudContainer.Models;

namespace TagCloudGUI.Interfaces
{
    public interface ICloudDrawer
    {
        void DrawCloudFromPalette(IEnumerable<RectangleWithText> rectangles, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette);
    }
}
