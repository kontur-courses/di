using System.Drawing;

namespace TagsCloudVisualization.SourcesTypes
{
    public class DrawableWord
    {
        public DrawableWord(string value, Rectangle place)
        {
            Value = value;
            Place = place;
        }

        public Rectangle Place { get; }
        public string Value { get; }
    }
}