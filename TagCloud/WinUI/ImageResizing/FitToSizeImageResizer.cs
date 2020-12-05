using System.Drawing;
using WinUI.Helpers;

namespace WinUI.ImageResizing
{
    public class FitToSizeImageResizer : IImageResizer
    {
        public Image Resize(Image source, Size newSize) => GraphicsUtils.PlaceAtCenter(source, newSize);
    }
}