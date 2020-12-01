using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный цвет")]
    public class RandomColorSource : IColorSource
    {
        protected readonly Color[] Colors;

        private readonly Random random = new Random();

        public RandomColorSource()
        {
            Colors = Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Select(Color.FromKnownColor)
                .ToArray();
        }

        protected RandomColorSource(Color[] colors)
        {
            this.Colors = colors;
        }

        public virtual Color BackgroundColor => Color.Black;

        public Brush GetBrush(string word, double distanceFromCenter)
        {
            var index = random.Next(0, Colors.Length);
            return new SolidBrush(Colors[index]);
        }

        private Color GetRandomColor()
        {
            var index = random.Next(0, Colors.Length);
            return Colors[index];
        }
    }
}