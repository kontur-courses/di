using System.Collections.Generic;
using System.Drawing;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    public interface IPrinter<TPrintedObject>
    {
        Result<Bitmap> GetBitmap(IColorScheme colorScheme, IEnumerable<TPrintedObject> objects, Size? bitmapSize = null);
        Result<Bitmap> GetBitmap(IColorScheme colorScheme, Result<IEnumerable<TPrintedObject>> objects, Size? bitmapSize = null);
    }
}