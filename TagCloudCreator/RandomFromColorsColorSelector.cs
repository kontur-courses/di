using System;
using System.Drawing;

namespace TagCloudCreator
{
    public class RandomFromColorsColorSelector : IColorSelector
    {
        private readonly Random random = new Random();
        public string Name => "Random from known colors";

        public Color GetColor(DrawingWord word)
        {
            var colors = Enum.GetValues(typeof(KnownColor));
            return Color.FromKnownColor((KnownColor) colors.GetValue(random.Next(0, colors.Length - 1)));
        }
    }
}