using System.Drawing;

namespace TagsCloudPainter.FormPointer;

public interface IFormPointer : IResetable
{
    Point GetNextPoint();
}