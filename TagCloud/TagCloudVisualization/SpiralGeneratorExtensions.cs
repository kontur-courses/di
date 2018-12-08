using System.Collections.Generic;
using System.Linq;

namespace TagCloudVisualization
{
    public static class SpiralGeneratorExtensions
    {
        public static IEnumerable<Point> Take(this AbstractSpiralGenerator generator, int amount) =>
            Enumerable.Range(0, amount)
                      .Select(_ => generator.Next());
    }
}
