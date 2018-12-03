using System.Drawing;

namespace TagCloud.Data
{
    public class Arguments
    {
        public string WordsFileName;
        public string BoringWordsFileName;
        public string ImageFileName;
        public int Multiplier = 10;
        public Brush WordsBrush = Brushes.Black;
        public Brush BackgroundBrush = Brushes.White;
        public FontFamily FontFamily = FontFamily.GenericMonospace;
    }
}