using System.Drawing;

namespace TagsCloudVisualization
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