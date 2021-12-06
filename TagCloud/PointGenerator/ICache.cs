using System.Drawing;

namespace TagsCloudVisualization.PointGenerator
{
    public interface ICache
    {
        float SafeGetParameter(Size size);
        void UpdateParameter(Size size, float radius);
    }
}