using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudTag
    {
        public Rectangle Rectangle { get;}
        public string Text { get; }
    }
}