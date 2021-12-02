using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IStylerSettings
{
    (Font font, Brush brush) GetStyle(ITag tag);
}