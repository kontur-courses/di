using System;
using System.Drawing;

namespace TagCloudVisualization
{
    public class WordInfo
    {
        private float scale;

        public WordInfo(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }

        public int Count { get; }

        public Rectangle Rectangle { get; private set; }

        public float Scale { get => scale; private set => scale = Math.Abs(value - 1) < 0.001 ? Word.Length : value; }

        public override string ToString() => $"{{{Word}, {Rectangle}, {Count}, {Scale}}}";


        public WordInfo With(Rectangle rectangle) => new WordInfo(Word, Count) {Rectangle = rectangle, Scale = Scale};

        public WordInfo With(float scalingNumber) =>
            new WordInfo(Word, Count) {Scale = scalingNumber, Rectangle = Rectangle};
    }
}
