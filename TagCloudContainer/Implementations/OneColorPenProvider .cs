using System.Drawing;
using TagCloudContainer.Api;

namespace TagCloudContainer.Implementations
{
    public class OneColorPenProvider : IRectanglePenProvider
    {

        public Pen CreatePenForRectangle(Rectangle rectangle)
        {
            return new Pen(Color.Blue);
        }
    }
}