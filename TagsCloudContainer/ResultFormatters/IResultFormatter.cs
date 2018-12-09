using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultFormatters
{
    public interface IResultFormatter
    {
        IReadOnlyDictionary<string, (Rectangle, int)> Rectangles { get; set; }
        void GenerateResult(Size size, FontFamily fontFamily, Brush brush, string outputFileName);
    }
}
