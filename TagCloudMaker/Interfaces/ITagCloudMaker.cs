using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud
{
    public interface ITagCloudMaker
    {
        IEnumerable<TextRectangle> CreateTagCloud(IEnumerable<string> words, int minLetterSize, string pathToSave);

        Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings);

        string SaveTagCloud(Image image, ImageFormat format);
    }
}