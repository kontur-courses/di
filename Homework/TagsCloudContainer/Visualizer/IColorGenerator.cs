using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IColorGenerator
    {
        public Stack<Color> GetColors(int count);

    }
}