using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ISpiral
    {
        public Point GetNextPoint();
        public Point Center { get; }
    }
}