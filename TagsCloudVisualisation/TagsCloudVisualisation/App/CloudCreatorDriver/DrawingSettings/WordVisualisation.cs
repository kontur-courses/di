using System.Drawing;

namespace TagsCloudVisualisation.App.DrawingSettings
{
    public class WordVisualisation : IWordVisualisation
    {
        private readonly Color color;
        private readonly double startValue;
        private readonly Font font;
        
        public WordVisualisation(Color color, double startValue, Font font)
        {
            this.color = color;
            this.startValue = startValue;
            this.font = font;
        }

        public Color GetColor()
        {
            return color;
        }

        public double GetStartingValue()
        {
            return startValue;
        }

        public Font GetFont()
        {
            return font;
        }
    }
}