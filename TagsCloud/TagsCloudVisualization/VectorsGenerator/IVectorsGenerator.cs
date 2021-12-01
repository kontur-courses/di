using System.Drawing;

namespace TagsCloudVisualization.VectorsGenerator
{
    public interface IVectorsGenerator
    {
        Point GetNextVector();
    }
}