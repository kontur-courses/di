using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPrinter<in TPrintedObject>
    {
        Bitmap GetBitmap(IEnumerable<TPrintedObject> objects, Size? bitmapSize = null);
    }
}