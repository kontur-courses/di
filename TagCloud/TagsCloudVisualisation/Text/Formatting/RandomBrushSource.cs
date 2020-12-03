using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный цвет")]
    public class RandomBrushSource : IBrushSource
    {
        protected readonly Brush[] Brushes;

        private readonly Random random = new Random();

        public RandomBrushSource()
        {
            Brushes = Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Select(Color.FromKnownColor)
                .Select(x => new SolidBrush(x))
                .Cast<Brush>()
                .ToArray();
        }

        protected RandomBrushSource(Color[] colors)
        {
            Brushes = colors.Select(x => new SolidBrush(x))
                .Cast<Brush>()
                .ToArray();
        }

        public virtual Color BackgroundColor => Color.Black;

        public Brush GetBrush(string _, double __) => RandomBrush();

        public Brush RandomBrush() => Brushes[random.Next(0, Brushes.Length)];
    }
}