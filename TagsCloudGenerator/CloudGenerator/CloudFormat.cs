using System.Drawing;

namespace TagsCloudGenerator
{
    public class CloudFormat
    {
        public readonly StringFormat TagTextFormat;
        public readonly Font TagTextFont;
        public readonly ICloudColorPainter ColorPainter;
        public readonly ITagOrder TagOrderPreform; 

        public CloudFormat(StringFormat tagTextFormat, Font tagTextFont, 
            ICloudColorPainter colorPainter, ITagOrder tagOrderPreform)
        {
            TagTextFormat = tagTextFormat;
            TagTextFont = tagTextFont;
            ColorPainter = colorPainter;
            TagOrderPreform = tagOrderPreform;
        }
    }
}