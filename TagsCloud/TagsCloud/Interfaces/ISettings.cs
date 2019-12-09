using System.Drawing;

namespace TagsCloudGenerator.Interfaces
{
    public interface ISettings : IResettable
    {
        string Font { get; set; }
        Size? ImageSize { get; set; }
        IPainterSettings PainterSettings { get; set; }
        IFactorySettings FactorySettings { get; set; }
    }
}