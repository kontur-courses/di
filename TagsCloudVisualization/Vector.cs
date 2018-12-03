using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Vector
    {
        public Point Position { get; }

        public Vector(Point position)
            => Position = position;

        public Vector(Point startPoint, Point endPoint)
            => Position = endPoint - startPoint;

        public Vector Normalized()
            => new Vector(Position.Normalized());
    }
}
