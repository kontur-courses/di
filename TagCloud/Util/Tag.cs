using System.Drawing;

namespace TagCloud.Util
{
    public class Tag
    {
        private readonly TagStat stat;

        public string Word => stat.Word;
        public int RepeatsCount => stat.RepeatsCount;
        public Font Font { get; }
        public RectangleF Place { get; }
        public Brush Brush { get; set; }

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