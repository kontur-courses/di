using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface ITagColoringFactory
    {
        ITagColoring Create(string algorithmName, IEnumerable<Color> colors);
    }
}
