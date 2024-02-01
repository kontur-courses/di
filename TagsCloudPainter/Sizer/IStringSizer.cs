using System.Drawing;

namespace TagsCloudPainter.Sizer;

public interface IStringSizer
{
    Size GetStringSize(string value, string fontName, float fontSize);
}