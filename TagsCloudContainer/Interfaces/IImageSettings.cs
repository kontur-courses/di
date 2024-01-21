using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IImageSettings
    {
        Color BackgroundColor { get; }
        Color FontColor { get; }
        Font GetFont();
        int ImageWidth { get; }
        int ImageHeight { get; }
    }
}
