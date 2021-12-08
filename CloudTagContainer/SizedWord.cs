using System.Drawing;

namespace CloudTagContainer
{
    public class SizedWord
    {
        public string Word { get; }
        public Size WordSize { get; }

        public SizedWord(string word, Size size)
        {
            this.Word = word;
            this.WordSize = size;
        }
    }
}