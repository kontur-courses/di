using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    public sealed class CloudTag
    {
        public readonly Rectangle Shape;
        public readonly string Text;
        public readonly StringFormat Format;
        public readonly Font TextFont;

        public CloudTag(Rectangle shape, string text, StringFormat format, Font textFont)
        {
            Shape = shape;
            Text = text;
            Format = format;
            TextFont = textFont;
        }
    }
}