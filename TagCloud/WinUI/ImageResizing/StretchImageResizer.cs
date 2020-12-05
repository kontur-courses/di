using System.Drawing;

namespace WinUI.ImageResizing
{
    public class StretchImageResizer : IImageResizer
    {
        public Image Resize(Image source, Size newSize) => new Bitmap(source, newSize);
    }
}