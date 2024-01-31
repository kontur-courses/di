using System.Drawing;

namespace TagCloudDi.Layouter
{
    public class RectangleData
    {
        public readonly Rectangle rectangle;
        public readonly string word;
        public readonly float fontSize;

        public RectangleData(Rectangle rectangle, string word, float fontSize)
        {
            this.fontSize = fontSize;
            this.word = word;
            this.rectangle = rectangle;
        }
    }
}
