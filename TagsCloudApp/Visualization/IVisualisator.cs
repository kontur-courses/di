using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApp.Visualization
{
    public interface IVisualisator
    {
        Bitmap Visualize(IEnumerable<Tuple<string, Rectangle>> wordRectanglePairs, Size bitmpapSize
                                , Color currentColor, Font currentFont);
    }
}
