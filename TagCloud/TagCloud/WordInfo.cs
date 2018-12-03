using System.Drawing;

namespace TagCloud
{
    internal class WordInfo
    {
        public WordInfo(string word, int count)
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }

        


        public Size CreateRectangle()
        {
            return new Size(50*Count*Word.Length, 50*Count);
        }
    }
}