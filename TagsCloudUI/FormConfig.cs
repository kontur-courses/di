using System.Drawing;

namespace TagsCloudUI
{
    public class FormConfig
    {
        public Color BackgroundColor { get; }
        public Brush TextColor { get; }
        public string FontFamily { get; }
        public Size FormSize { get; }

        public FormConfig(Color backgroundColor, Brush textColor, string fontFamily, Size formSize)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            FontFamily = fontFamily;
            FormSize = formSize;
        }
    }
}