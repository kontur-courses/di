using System.Drawing;

namespace WinUI.ImageResizing
{
    public interface IImageResizer
    {
        Image Resize(Image source, Size newSize);
    }
}