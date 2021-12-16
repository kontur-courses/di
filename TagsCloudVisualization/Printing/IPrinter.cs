using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPrinter<in TPrintedObject>
    {
        Bitmap GetBitmap(IColorScheme colorScheme, IEnumerable<TPrintedObject> objects, Size? bitmapSize = null);
    }
}