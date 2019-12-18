using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualization.Measurers;

namespace TagsCloudContainer.Visualization.Painters
{
    public class SteppedColorPainter : IPainter
    {
        public const string Name = "stepped";
        
        private readonly Brush primaryBrush;
        private readonly Brush majorityBrush;
        private readonly Brush minorityBrush;

        public SteppedColorPainter(ISettings settings) :
            this(settings.PrimaryColor, settings.MajorityColor, settings.MinorityColor)
        {
        }

        public SteppedColorPainter(Color primary, Color majority, Color minority)
        {
            primaryBrush = new SolidBrush(primary);
            majorityBrush = new SolidBrush(majority);
            minorityBrush = new SolidBrush(minority);
        }

        public ColorizedRectangle[] Colorize(Rectangle[] rectangles)
        {
            var half = rectangles.Length / 2;
            return rectangles
                .Select((r, i) => new ColorizedRectangle(r, GetBrush(i, half), Pens.Black))
                .ToArray();
        }

        public ColorizedTag[] Colorize(Tag[] tags)
        {
            var half = tags.Length / 2;
            return tags
                .Select((t, i) =>
                    new ColorizedTag(t, GetBrush(i, half), Brushes.Transparent, Pens.Transparent))
                .ToArray();
        }

        private Brush GetBrush(int index, int half)
        {
            return index == 0 ? primaryBrush : index < half ? majorityBrush : minorityBrush;
        }

        public interface ISettings
        {
            Color PrimaryColor { get; set; }
            Color MajorityColor { get; set; }
            Color MinorityColor { get; set; }
        }
    }
}