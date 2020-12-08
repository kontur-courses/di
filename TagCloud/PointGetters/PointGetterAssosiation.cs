using System.Collections.Generic;

namespace TagCloud.PointGetters
{
    public static class PointGetterAssosiation
    {
        private static readonly Dictionary<string, IPointGetter> pointGetters =
            new Dictionary<string, IPointGetter>
            {
                ["circle"] = new CirclePointGetter(),
                ["spiral"] = new SpiralPointGetter()
            };

        public static IPointGetter GetPointGetter(string name) =>
            pointGetters.TryGetValue(name, out var getter) ? getter : null;
    }
}
