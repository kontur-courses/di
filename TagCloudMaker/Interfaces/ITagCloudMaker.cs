using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Interfaces
{
    public interface ITagCloudMaker
    {
        IEnumerable<TextRectangle> CreateTagCloud(string filePath, int minLetterSize, string pathToSave);

        Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings);

        string SaveTagCloud(Image image, ImageFormat format);
    }
}