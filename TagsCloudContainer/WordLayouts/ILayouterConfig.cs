using System.Drawing;

namespace TagsCloudContainer.WordLayouts
{
    public interface ILayouterConfig
    {
        PointF CenterPoint { get; }

        double AngleDelta { get; }
    }
}