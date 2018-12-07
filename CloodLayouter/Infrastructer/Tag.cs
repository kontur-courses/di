using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public class Tag
    {
        public Font Font{ get; set; }
        public string Word { get; set; }

        public Tag(string word = "",Font font = null)
        {
            Word = word;
            Font = font;
        }
        
    }
}