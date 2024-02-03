using System.Drawing;
using TagsCloudVisualization;
namespace TagCloud;

public interface IPointsFactory
{
    IPoints Get(Point cloudCenter);
}