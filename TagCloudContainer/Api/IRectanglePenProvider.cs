using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface IRectanglePenProvider
    {
        Pen CreatePenForRectangle(Rectangle rectangle);
    }
}