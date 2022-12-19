using System.Drawing;

namespace TagCloudContainer.Interfaces
{
    public interface IPointer
    {
        Point GetNextPoint();
        void Reset();
    }
}
