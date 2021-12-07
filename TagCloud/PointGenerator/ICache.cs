using System.Drawing;

namespace TagCloud.PointGenerator
{
    public interface ICache
    {
        float SafeGetParameter(Size size);
        void UpdateParameter(Size size, float radius);
    }
}