using System.Drawing;

namespace TagCloud.TextColoration
{
    public class SimpleTextColoration : ITextColoration
    {
        public Brush GetTextColor(string word, int frequency)
        {
            return frequency <= 2 ? Brushes.Chocolate :
                frequency <= 5 ? Brushes.Orange :
                frequency <= 10 ? Brushes.OrangeRed :
                frequency <= 20 ? Brushes.Red : Brushes.Crimson;
        }
    }
}