using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IPointGenerator
{
    Point Next();
}