using System.Drawing;

namespace TagsCloudContainer.Abstractions;

public interface IStyledTag
{
    Size GetTrueGraphicSize(Graphics graphics);
    void DrawSelf(Graphics graphics, Rectangle position);
}