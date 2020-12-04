using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudUI
{
    public class FormConfig
    {
        public FormConfig(Color backgroundColor, Brush textColor, string fontFamily, Size formSize, SpiralType spiralType)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            FontFamily = fontFamily;
            FormSize = formSize;
            SpiralType = spiralType;
        }

        public Color BackgroundColor { get; }
        public Brush TextColor { get; }
        public string FontFamily { get; }
        public Size FormSize { get; }
        public SpiralType SpiralType { get; }
    }
}