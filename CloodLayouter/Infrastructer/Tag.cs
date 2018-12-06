using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public class Tag
    {
        public Size Size { get; set; }
        public string Word { get; set; }

        public Tag(string word = "", Size size = new Size())
        {
            Word = word;
            Size = size;
        }
        
    }
}