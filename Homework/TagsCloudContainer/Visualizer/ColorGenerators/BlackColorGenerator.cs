using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer.ColorGenerators
{
    public class BlackColorGenerator : IColorGenerator
    {
        public PalleteType PalleteType => PalleteType.Black;

        public Stack<Color> GetColors(int count)
        {
            var colors = new Stack<Color>();
            for (var i = 0; i < count; i++) colors.Push(Color.Black);

            return colors;
        }
    }
}