using System.Drawing;

namespace TagsCloudContainer.ImageCreators
{
    public interface IPalette
    {
        Color FontColor { get; set; }
        Color BackGroundColor { get; set; }
    }
}