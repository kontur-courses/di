using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IImageSettings
    {
        Color BackgroundColor { get; }
        Color FontColor { get; }
        Font GetFont();
        int Width { get; set; }
        int Height { get; set; }

        void UpdateImageSettings(int width, int height);
    }
}
