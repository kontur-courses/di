using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Visualizing.ColorHandling
{
    public class AlternateColorHandler : IColorHandler
    {
        private List<Color> colors = new List<Color>();
        private static readonly Color DefaultBackgroundColor = Color.White;
        private static readonly Color DefaultWordColor = Color.Black;
        private int currentPosition = 1;

        public Color GetColorFor(string word, Rectangle rectangle)
        {
            if (colors.Count == 0)
                return DefaultWordColor;
            if (colors.Count == 1)
                return colors.First();

            if (currentPosition == colors.Count)
                currentPosition = 1;
            currentPosition++;
            return colors[currentPosition - 1];
        }

        public Color BackgroundColor => colors.Count <= 1 ? DefaultBackgroundColor : colors.First();
        
        public void SetColorsToUse(List<Color> colorsToUse)
        {
            colors = colorsToUse;
        }
    }
}