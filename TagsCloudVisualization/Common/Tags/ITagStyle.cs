using System.Drawing;

namespace TagsCloudVisualization.Common.Tags
{
    public interface ITagStyle
    {
        public Color ForegroundColor { get; set; }
        public Font Font { get; set; }
    }
}