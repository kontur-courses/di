using System.Drawing;

namespace TagsCloudVisualization.SourcesTypes
{
    public class Sizable<T>
    {
        public Sizable(T value, Size drawSize)
        {
            DrawSize = drawSize;
            Value = value;
        }

        public Size DrawSize { get; }
        public T Value { get; }
    }
}