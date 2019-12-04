using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud
{
    public interface ICloud
    {
        List<TagRectangle> GetRectangles(int width, int height, string path = null);
    }
}
