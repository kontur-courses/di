using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IStyledTag
{
    Size GetTrueGraphicSize(Graphics graphics);
    void DrawSelf(Graphics graphics, Rectangle position);
}