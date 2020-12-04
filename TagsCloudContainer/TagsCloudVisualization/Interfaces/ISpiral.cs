using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ISpiral
    {
        public Point Center { get; }
        public Point GetNextPoint();
    }
}