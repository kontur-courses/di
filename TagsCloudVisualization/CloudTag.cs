using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public class CloudTag
{
    public string InnerText { get; init; }
    public Font TextFont { get; set; }
    public Color TextColor { get; set; }
    public bool IsRotated { get; set; }
    public RectangleF BoundRectangle { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is CloudTag cloudTag && cloudTag.InnerText.Equals(InnerText);
    }

    public override int GetHashCode()
    {
        return InnerText.GetHashCode();
    }
}