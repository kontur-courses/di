#region

using System.Drawing;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class Tag : ITag
    {
        public Tag(Rectangle rectangle, string word, int frequency)
        {
            Rectangle = rectangle;
            Word = word;
            Frequency = frequency;
        }

        public Rectangle Rectangle { get; }
        public string Word { get; }
        public int Frequency { get; }
    }
}