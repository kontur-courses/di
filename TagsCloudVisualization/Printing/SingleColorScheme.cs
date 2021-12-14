using System.Drawing;

namespace TagsCloudVisualization
{
    public class SingleColorScheme : IColorScheme
    {
        private readonly Color color;

        public SingleColorScheme(Color color)
        {
            this.color = color;
        }
        
        public Color GetColorBy(Size size)
        {
            return color;
        }
    }
}