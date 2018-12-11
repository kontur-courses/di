using System.Drawing;

namespace TagCloud.Core.Util
{
    public class Tag
    {
        private readonly TagStat stat;

        public Font Font { get; set; }
        public RectangleF Place { get; set; }
        public Brush Brush { get; set; }

        public string Word => stat.Word;
        public int RepeatsCount => stat.RepeatsCount;

        public Tag(TagStat stat, Font font, RectangleF place, Brush brush)
            : this(stat, font, place)
        {
            Brush = brush;
        }

        public Tag(TagStat stat, Font font, RectangleF place)
        {
            this.stat = stat;
            Font = font;
            Place = place;
        }
    }
}