using System.Drawing;

namespace TagsCloudVisualization.InterfacesForSettings
{
    public interface IImageSettings
    {
        Size ImageSize { get; set; }
        Point Center { get; set; }
        Font Font { get; set; }
    }
}