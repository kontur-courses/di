using System.Collections.Generic;
using System.Drawing;
namespace TagCloud;

public interface ICloudDrawer
{
    Bitmap DrawCloud(IEnumerable<WordForCloud> wordsForCloud);
}