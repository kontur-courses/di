using System.Drawing;

namespace TagsCloudVisualization.SourcesTypes
{
    public class SizableWord
    {
        public SizableWord(string value, Size drawSize)
        {
            DrawSize = drawSize;
            Value = value;
        }

        public Size DrawSize { get; }
        public string Value { get; }
    }
}