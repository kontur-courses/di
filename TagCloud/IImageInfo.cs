using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface IImageInfo
    {
        List<Tuple<String, Rectangle>> GetTags(string filename, int canvasHeight);
    }
}