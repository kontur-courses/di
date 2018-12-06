using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPolar
    {
        Point Center { get; }
        Point GetNextPoint();
    }
}