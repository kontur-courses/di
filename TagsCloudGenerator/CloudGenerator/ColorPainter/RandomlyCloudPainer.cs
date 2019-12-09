using System;
using System.Drawing;

namespace TagsCloudGenerator
{
    public class RandomlyCloudPainer : ICloudColorPainter
    {
        protected Color[] Palette;
        protected Random Randomizer;
        protected int GetRandomIndex() => Randomizer.Next(Palette.Length - 1);

        public RandomlyCloudPainer(Color[] paletteColors, Color backgroundColor)
        {
            if (paletteColors.Length == 0)
                throw new ArgumentException("Colors palette can't be zero");
            
            Palette = paletteColors;
            BackgroundColor = backgroundColor;
            Randomizer = new Random();
        }

        public Color GetTagShapeColor()
        {
            return Palette[GetRandomIndex()];
        }

        public Color GetTagTextColor(Color shapeColor)
        {
            Color newColor;

            do
            {
                newColor = Palette[GetRandomIndex()];
            } 
            while (shapeColor == newColor && Palette.Length != 1);

            return newColor;
        }

        public Color BackgroundColor { get; }
    }
}