using System.Drawing;

namespace TagCloudContainer.PointAlgorithm
{
    public interface IPointer
    {
        Point GetNextPoint();
        IPointConfig Config { get; set; }
        void Reset();
    }
}
