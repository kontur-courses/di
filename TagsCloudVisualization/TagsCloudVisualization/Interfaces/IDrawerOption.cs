using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface IDrawerOption
    {
        SolidBrush TextBrush { get; }
        Color BackgroundColor { get; }
        Font TextOption { get; }
    }
}