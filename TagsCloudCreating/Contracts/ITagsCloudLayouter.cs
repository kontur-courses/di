using System.Drawing;
using TagsCloudLayouters.Configuration;

namespace TagsCloudLayouters.Contracts
{
    public interface ITagsCloudLayouter
    {
        public CloudLayouterSettings LayouterSettings { get; }
        public Rectangle PutNextRectangle(Size size);
        public void Recreate();
    }
}