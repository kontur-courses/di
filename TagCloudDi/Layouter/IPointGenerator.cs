using System.Drawing;

namespace TagCloudDi.Layouter
{
    public interface IPointGenerator
    {
        public Point GetNextPoint();
        public Point CenterPoint { get; }
    }
}
