using System.Drawing;
using TagCloud.Core.Utils;
using TagCloud.Gui.Helpers;

namespace TagCloud.Gui.ImageResizing
{
    public class FitToSizeImageResizer : IImageResizer
    {
        public Image Resize(Image source, Size newSize) => GraphicsUtils.FitToSize(source, newSize);
    }
}