namespace TagCloud.Data
{
    public class Arguments
    {
        public string WordsFileName = @"..\..\Default\text.txt";
        public string BoringWordsFileName = @"..\..\Default\boringWords.txt";
        public string ImageFileName = "tagCloud.png";
        public int Multiplier = 10;
        public string WordsColorName = "White";
        public string BackgroundColorName = "Black";
        public string FontFamilyName = "Arial";
        public bool ToEnableClipboardSaver = false;
    }
}