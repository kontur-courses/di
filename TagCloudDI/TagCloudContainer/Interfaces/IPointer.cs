using System.Drawing;

namespace TagCloudContainer.Interfaces
{
    public interface IPointProvider
    {
        Point GetNextPoint();
        void Reset();
    }
}
