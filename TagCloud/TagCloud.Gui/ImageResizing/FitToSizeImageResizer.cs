using System.Drawing;
using TagCloud.Gui.Helpers;

namespace TagCloud.Gui.ImageResizing
{
    public class FitToSizeImageResizer : IImageResizer
    {
        public Image Resize(Image source, Size newSize) => GraphicsUtils.PlaceAtCenter(source, newSize);
    }
}