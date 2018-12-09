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

        public WordInfo With(string changedWord)
        {
            return new WordInfo(changedWord, Count);
        }

        public WordInfo With(Func<string, string> wordChanger)
        {
            return With(wordChanger(Word));
        }

        public string Word { get; }
        public int Count { get; }
        public Rectangle Rectangle { get;  }

        public void Deconstruct(out Rectangle rectangle, out string word)
        {
            rectangle = Rectangle;
            word = Word;
        }

        public WordInfo With(Rectangle rectangle)
        {
            return new WordInfo(Word, Count, rectangle);
        }
    }
}
