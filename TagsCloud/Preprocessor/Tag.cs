using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public string Value { get; }
        public int Frequency { get; }

        public Tag(string value, int frequency)
        {
            Value = value;
            Frequency = frequency;
        }
    }
}