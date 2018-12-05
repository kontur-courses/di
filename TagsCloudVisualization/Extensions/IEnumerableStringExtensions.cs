using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace TagsCloudVisualization
{
    internal static class IEnumerableStringExtensions
    {
        internal static Point ToPoint(this IEnumerable<string> list)
        {
            var result = list.ToArray();
            return new Point(int.Parse(result[0]), int.Parse(result[1]));
        }
        
        internal static Spiral ToSpiral(this IEnumerable<string> list)
        {
            var result = list.ToArray();
            return new Spiral(double.Parse(result[0], CultureInfo.InvariantCulture), int.Parse(result[1]));
        }  
    }
}