using System.Drawing;

namespace TagsCloudVisualization.Common.Tags
{
    public class TagStyle
    {
        public Color ForegroundColor { get; set; }
        public Font Font { get; set; }

        public TagStyle()
        {
            ForegroundColor = Color.White;
            Font = new Font("Arial", 14);
        }
        
        public TagStyle(Color foregroundColor, Font font)
        {
            ForegroundColor = foregroundColor;
            Font = font;
        }
    }
}