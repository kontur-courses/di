using System.Drawing;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class OneColorPenProvider : IRectanglePenProvider
    {
        private readonly DrawingOptions options;

        public OneColorPenProvider(DrawingOptions options)
        {
            this.options = options;
        }

        public Pen CreatePenForRectangle(Rectangle rectangle)
        {
            return options.Pen;
        }
    }
}