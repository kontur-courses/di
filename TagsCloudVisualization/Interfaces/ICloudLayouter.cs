using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(string word, int fontSize);
        IEnumerable<Rectangle> PutRectangles(IEnumerable<string> words, int fontSize);
        Rectangle[] GetPutRectangles();
        Point GetCenter();
    }
}