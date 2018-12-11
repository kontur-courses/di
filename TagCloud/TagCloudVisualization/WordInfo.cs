using System;
using System.Drawing;

namespace TagCloudVisualization
{
    public class WordInfo
    {
        public WordInfo(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }

        public int Count { get; }

        public Rectangle Rectangle { get; private set; }

        public float Scale { get; private set; }

        public override string ToString() => $"{{{Word}, {Rectangle}, {Count}, {Scale}}}";

        public WordInfo With(string changedWord) => new WordInfo(changedWord, Count);

        public WordInfo With(Func<string, string> wordChanger) => With(wordChanger(Word));

        public void Deconstruct(out Rectangle rectangle, out string word, out float scale)
        {
            rectangle = Rectangle;
            word = Word;
            scale = Scale;
        }

        public WordInfo With(Rectangle rectangle) => new WordInfo(Word, Count) {Rectangle = rectangle, Scale = Scale};

        public WordInfo With(float scalingNumber) =>
            new WordInfo(Word, Count) {Scale = scalingNumber, Rectangle = Rectangle};
    }
}
