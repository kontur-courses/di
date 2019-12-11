using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizer
    { 
        Image DrawImage(IList<WordRectangle> wordRectangles, Size imageSize);
    }
}
