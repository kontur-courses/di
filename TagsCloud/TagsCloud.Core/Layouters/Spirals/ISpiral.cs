using System.Drawing;

namespace TagsCloud.Core.Layouters.Spirals;

internal interface ISpiral
{
    public Point GetNextPoint();
}