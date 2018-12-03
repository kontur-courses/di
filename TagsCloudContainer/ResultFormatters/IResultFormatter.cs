using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ResultFormatters
{
    public interface IResultFormatter
    {
        void GenerateResult(Size size, Font font, Brush brush, string outputFileName,
            Dictionary<string, (Rectangle, int)> rectangles);
    }
}
