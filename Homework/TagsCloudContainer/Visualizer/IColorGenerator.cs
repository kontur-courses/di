using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public interface IColorGenerator
    {
        public IEnumerable<Color> GetColor();

    }
}