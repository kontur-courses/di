using System.Drawing;

namespace TagsCloudContainer.LayouterAlgorithms
{
    public interface ICloudLayouterAlgorithm
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}