using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public IRectangleComposer Composer { get; set; }

        public CircularCloudLayouter(IRectangleComposer composer)
        {
            Composer = composer;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (CheckSizeAvailable(rectangleSize))
            {
                var placedRectangle = Composer.GetNextRectangleInCloud(rectangleSize);
                return placedRectangle;
            }

            return Rectangle.Empty;
        }

        public bool CheckSizeAvailable(Size checkAvailable)
        {
            return (checkAvailable.Width > 0 && checkAvailable.Height > 0);
        }
    }
}