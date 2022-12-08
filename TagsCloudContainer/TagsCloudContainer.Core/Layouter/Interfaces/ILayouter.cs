using System.Drawing;

namespace TagsCloudContainer.Core.Layouter.Interfaces
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);

        public void SetCenter(Point center);
    }
}
