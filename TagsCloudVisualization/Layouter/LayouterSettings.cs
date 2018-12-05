using System.Drawing;
using NSubstitute.Core;

namespace TagsCloudVisualization
{
    public class LayouterSettings
    {
        public Point Center { get; } 
        public Spiral Spiral { get; } 

        public LayouterSettings(Point centralPoint, Spiral spiral)
        {
            Center = centralPoint;
            Spiral = spiral;
        }

    }
}