using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class Tag : ITag
    {
        public RectangleF Layout { get; private set; }
        public Palette Palette { get; set; } = Palette.DefaultPalette;
        public string Text { get; }

        public delegate Tag Factory(string text);

        public Tag(string text)
        {
            Text = text;
        }

        public void ReplaceTagLayout(RectangleF layout)
        {
            Layout = layout;
        }
    }
}