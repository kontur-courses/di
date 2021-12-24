using System.Drawing;

namespace TagsCloudVisualization.Printing
{
    internal class SingleColorScheme : IColorScheme
    {
        private Color color;

        public void SetColor(Color colour)
        {
            color = colour;
        }
        
        public Color GetColorBy(Size size)
        {
            return color;
        }
    }
}