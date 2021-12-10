using System.Drawing;

namespace TagCloud.PointGenerator
{
    public interface ICache
    {
        float SafeGetParameter(SizeF size);
        void UpdateParameter(SizeF size, float radius);
    }
}