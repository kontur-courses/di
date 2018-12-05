using System.Drawing;

namespace TagCloud
{
    internal class WordInfo
    {
        public WordInfo(string word, int count)
        {
            this.Word = word;
            this.Count = count;
        }

        public string Word { get; }
        public int Count { get; }

        public Size CreateRectangle() => new Size(50 * this.Count * this.Word.Length, 50 * this.Count);
    }
}
