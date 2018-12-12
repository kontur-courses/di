using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        void Process(IEnumerable<GraphicWord> graphicWord, ISizeDefiner sizeDefiner, Point center);
    }
}
