using System.Drawing;

namespace TagCloud.Infrastructure.Text.Information
{
    public class TokenInfo
    {
        public TokenInfo(WordType wordType = default, int frequency = default, int fontSize = default,
            Size size = default)
        {
            WordType = wordType;
            Frequency = frequency;
            FontSize = fontSize;
            Size = size;
        }

        public WordType WordType { get; set; }
        public int Frequency { get; set; }
        public int FontSize { get; set; }
        public Size Size { get; set; }
    }
}