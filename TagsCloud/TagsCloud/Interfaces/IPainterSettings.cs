using System.Drawing;

namespace TagsCloudGenerator.Interfaces
{
    public interface IPainterSettings : IResettable
    {
        Color[] Colors { get; set; }
        Color BackgroundColor { get; set; }
    }
}