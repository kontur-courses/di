using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer.ColorGenerators
{
    public interface IColorGenerator
    {
        public PalleteType PalleteType { get; }
        public Stack<Color> GetColors(int count);
    }
}