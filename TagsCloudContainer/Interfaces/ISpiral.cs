using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ISpiral
{
    Point GetNext();

    void Reset();
}
