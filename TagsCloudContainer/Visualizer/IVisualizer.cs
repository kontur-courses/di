using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    interface IVisualizer
    {
        Bitmap GetImage(IList<WordRectangle> wordRectangles);
    }
}
