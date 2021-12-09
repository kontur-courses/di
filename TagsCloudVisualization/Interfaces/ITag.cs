using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ITag
    {
        public RectangleF Layout { get; }
        public Palette Palette { get; }
        public string Text { get; }

        public delegate Tag Factory(string text, Palette palette);

        public void ReplaceTagLayout(RectangleF layout);
    }
}