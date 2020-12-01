using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Coloring
{
    public class BackgroundPainterEmpty: IBackgroundPainter
    {
        public void Draw(List<Tuple<string, Rectangle>> tags, ICanvas canvas, Graphics graphics)
        {
        }
    }
}