using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud
{
    public interface ICloud
    {
        List<TagRectangle> Rectangles { get; }
        ClientData Data { get; }
    }
}
