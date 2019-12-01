using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Options
    {
        public readonly Font Font;
        public readonly Size ImageSize;
        public readonly Color BackgroundColor;
        public readonly Color TextColor;

        public Options(Font font, Size imageSize, Color backgroundColor, Color textColor)
        {
            Font = font;
            ImageSize = imageSize;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
}