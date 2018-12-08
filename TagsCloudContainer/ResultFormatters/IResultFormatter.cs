using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultFormatters
{
    public interface IResultFormatter
    {
        void GenerateResult(Size size, FontFamily fontFamily, Brush brush, string outputFileName,
            IReadOnlyDictionary<string, (Rectangle, int)> rectangles);
    }
}
