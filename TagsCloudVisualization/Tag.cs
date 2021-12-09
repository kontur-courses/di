using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class Tag : ITag
    {
        public RectangleF Layout { get; private set; }
        public Palette Palette { get; }
        public string Text { get; }

        public delegate Tag Factory(string text, Palette palette);

        public Tag(string text, Palette palette)
        {
            Text = text;
            Palette = palette;
        }

        public void ReplaceTagLayout(RectangleF layout)
        {
            Layout = layout;
        }
    }
}