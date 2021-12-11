using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ITag
    {
        public RectangleF Layout { get; }
        public Palette Palette { get; set; }
        public string Text { get; }

        public delegate Tag Factory(string text);

        public void ReplaceTagLayout(RectangleF layout);
    }
}