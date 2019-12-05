using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Visualizer
{
    public interface IColoringAlgorithm
    {
        IEnumerable<Color> GetColors(ImageSettings imageSettings);
    }
}