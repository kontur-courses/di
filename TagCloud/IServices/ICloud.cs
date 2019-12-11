using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud
{
    public interface ICloud
    {
        List<TagRectangle> GetRectangles(Graphics graphics, ImageSettings imageSettings, string path = null);
    }
}