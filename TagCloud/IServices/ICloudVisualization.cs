using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud
{
    public interface ICloudVisualization
    {
        Dictionary<string, Palette> PaletteDictionary { get; }
        Bitmap GetAndDrawRectangles(ImageSettings imageSettings, string path);
    }
}