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

        private WordInfo(string word, int count, Rectangle rectangle) : this(word, count)
        {
            Rectangle = rectangle;
        }

        public string Word { get; }
        public int Count { get; }
        public Rectangle Rectangle { get; }

        public WordInfo With(string changedWord) => new WordInfo(changedWord, Count);

        public WordInfo With(Func<string, string> wordChanger) => With(wordChanger(Word));

        public void Deconstruct(out Rectangle rectangle, out string word)
        {
            rectangle = Rectangle;
            word = Word;
        }

        public WordInfo With(Rectangle rectangle) => new WordInfo(Word, Count, rectangle);
    }
}
