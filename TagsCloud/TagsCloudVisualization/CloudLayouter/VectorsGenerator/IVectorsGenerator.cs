using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.VectorsGenerator
{
    public interface IVectorsGenerator
    {
        Point GetNextVector();
    }
}