using System.Drawing;

namespace TagsCloudContainer.Infrastructure;

public record CloudTag : PaintedTag
{
    public Label Label;
    public Rectangle Rectangle;

    public CloudTag(PaintedTag original, Label label, Rectangle rectangle) : base(original)
    {
        Label = label;
        Rectangle = rectangle;
    }
}