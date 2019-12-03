using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualization
{
    public class ViewSettings
    {
        public readonly HashSet<Brush> colors = new HashSet<Brush>
        {
            Brushes.Aqua, Brushes.Lime, Brushes.Blue, Brushes.Brown, Brushes.Chartreuse,
            Brushes.Chocolate, Brushes.Coral, Brushes.Crimson, Brushes.MediumSlateBlue,
            Brushes.Gold, Brushes.Green, Brushes.Fuchsia, Brushes.BlueViolet
        };

        public string FontName { get; set; } = "PT Mono";
        public Color BackgroundColor { get; set; } = Color.Black;
        public Color TextColor { get; set; } = Color.Black;

        public bool EnableWordRectangles { get; set; } = true;

        public int WordsCount { get; set; } = 100;
    }
}