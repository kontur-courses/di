using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.Extensions;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный цвет")]
    public class RandomColorSource : IColorSource
    {
        private readonly Color[] colors;

        public RandomColorSource() : this(EnumerateKnownColors(), Color.Black)
        {
        }

        protected RandomColorSource(IEnumerable<Color> colors, Color background)
        {
            this.colors = colors.ToArray();
            Background = background;
        }

        public Color Background { get; }
        public Color GetWordColor() => Randomized.ItemOf(colors);

        private static IEnumerable<Color> EnumerateKnownColors() =>
            Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().Select(Color.FromKnownColor);
    }
}