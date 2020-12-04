namespace TagsCloudContainer.App.Settings
{
    public class FontSettings
    {
        public static readonly FontSettings Default = new FontSettings("Arial");

        private FontSettings(string fontName)
        {
            FontName = fontName;
        }

        public string FontName { get; set; }
    }
}