using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public string Word { get; }
        public int Count { get; }
        public PointF Location { get; set; }
        public RectangleF Area => new RectangleF(Location, Size);
        private SizeF Size { get; }

        public Tag(string word, int count, SizeF size, PointF location)
        {
            Word = word;
            Count = count;
            Size = size;
            Location = location;
        }
    }
}