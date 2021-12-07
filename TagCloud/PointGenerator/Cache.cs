using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.PointGenerator
{
    public class Cache : ICache
    {
        private readonly Dictionary<Size, float> sizeToCircleParameter = new();

        public float SafeGetParameter(Size size)
        {
            if (!sizeToCircleParameter.ContainsKey(size))
                sizeToCircleParameter[size] = 0;
            return sizeToCircleParameter[size];
        }

        public void UpdateParameter(Size size, float radius)
        {
            sizeToCircleParameter[size] = radius;
        }
    }
}