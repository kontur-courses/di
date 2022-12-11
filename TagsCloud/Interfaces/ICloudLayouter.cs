using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface ICloudLayouter
    {
        public IRectangleComposer Composer { get; set; }
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}
