using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public interface IPaletteSettings
    {
        Color BackgroundColor { get; }

        Color GetColorAccordingSize(float emSize);
    }
}