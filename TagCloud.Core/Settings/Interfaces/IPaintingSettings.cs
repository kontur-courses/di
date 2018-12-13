using System.Drawing;

namespace TagCloud.Core.Settings.Interfaces
{
    public interface IPaintingSettings
    {
        Color BackgroundColor { get; set; }
        Brush TagBrush { get; }
        Color TagColor { get; set; }
    }
}