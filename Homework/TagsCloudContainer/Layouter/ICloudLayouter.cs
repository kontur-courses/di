using System.Drawing;

namespace TagsCloudContainer.Layouters
{
    public interface ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size size);
    }
}