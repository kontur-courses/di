using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public static class RectangleFExtensions
    {
        public static bool IntersectsWith(this RectangleF current, IEnumerable<RectangleF> others)
        {
            return others.Any(current.IntersectsWith);
        }    
    }
}