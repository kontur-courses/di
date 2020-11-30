using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);

        public void SetCenter(int x, int y);
    }
}