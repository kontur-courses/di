using System.Drawing;

namespace TagCloud.Data
{
    public class Arguments
    {
        public string WordsFileName;
        public string BoringWordsFileName;
        public string ImageFileName;
        public int Multiplier = 10;
        public Color WordsColor = Color.Black;
        public Color BackgroundColor = Color.White;
        public FontFamily FontFamily = FontFamily.GenericMonospace;
    }
}