using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface ICloudLayouter
    {
        public ICloud<ITag> Cloud { get; }

        public void PutNextTag(SizeF tagLayouterSize, ITag tag);
    }
}