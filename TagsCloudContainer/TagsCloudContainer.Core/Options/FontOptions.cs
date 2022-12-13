namespace TagsCloudContainer.Core.Options
{
    public class FontOptions 
    {
        public string FontFamily { get; set; }
        public string FontColor { get; set; }

        public FontOptions(string fontFamily, string fontColor)
        {
            FontFamily = fontFamily;
            FontColor = fontColor;
        }
    }

}